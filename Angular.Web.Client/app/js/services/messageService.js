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

        var sendToChatroom = function(message) {
            return messageRequester('POST', baseUrl + 'Messages/Chatroom', "Content=" + message +
                "&ChatroomId=F0C58BCC-93C7-4663-8709-180205CA2F19");
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