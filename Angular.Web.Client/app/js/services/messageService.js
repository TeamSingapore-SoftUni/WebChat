webchatApp.factory('messageService', 
    function messageService($http, $q, baseUrl, authorizationService, hubService) {
        var headers = authorizationService.getAuthorizationHeaders();
            headers["Content-Type"] = "application/x-www-form-urlencoded";

        function messageRequester(method, url, data) {
            var deferred = $q.defer();
            $http({
                method: method,
                url: url,
                data: data,
                headers: headers
            })
                .success(function(data, status, headers, config) {
                    deferred.resolve(data, status, headers, config);
                })
                .error(function(data, status, headers, config) {
                    deferred.reject(data, status, headers, config);
                });

            return deferred.promise;
        }

        var getMessagesFromChatroom = function(chatroomId) {
            return messageRequester('GET', baseUrl + 'Messages/Chatroom?chatroomId=' + chatroomId);
        }

        var getMessagesWithUser = function(userId) {
            return messageRequester('GET', baseUrl + 'Messages/User?userId=' + userId);
        }

        var sendToChatroom = function(message, chatroomId) {
            return messageRequester('POST', baseUrl + 'Messages/Chatroom', "Content=" + message +
                "&ChatroomId=" + chatroomId);
        };

        var sendToUser = function(message, userId) {
            return messageRequester('POST', baseUrl + 'Messages/User', "Content=" + message +
                "&userId=" + userId);
        }

        var editMessage = function(message, messageId) {
            return messageRequester('PUT', baseUrl + 'Messages?id=' + messageId, "Content=" + message);
        }

        return {
            getMessagesFromChatroom: getMessagesFromChatroom,
            getMessagesWithUser: getMessagesWithUser,
            sendToChatroom: sendToChatroom,
            sendToUser: sendToUser,
            editMessage: editMessage
        };
});