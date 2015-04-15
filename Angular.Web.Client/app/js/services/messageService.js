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

        var sendToChatroom = function(message, chatroomId) {
            return messageRequester('POST', baseUrl + 'Messages/Chatroom', "Content=" + message +
                "&ChatroomId=" + chatroomId);
        };


        function sendToUser(userId, message)
        {

        }

        function getMessagesFromChatroom(chatroomId) {
            return messageRequester('GET', baseUrl + 'Messages/Chatroom?chatroomId=' + chatroomId);
        }

        return {
            sendToChatroom: sendToChatroom,
            getMessagesFromChatroom: getMessagesFromChatroom
        };
});