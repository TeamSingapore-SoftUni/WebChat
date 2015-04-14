webchatApp.factory('chatroomService',
    function messageService($http, $q, baseUrl, authorizationService, hubService) {
        var headers = authorizationService.getAuthorizationHeaders();
        headers["Content-Type"] = "application/x-www-form-urlencoded";

        function chatroomRequester(method, url, data) {
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

        var getAllChatrooms = function() {
            return chatroomRequester('GET', baseUrl + 'Chatroom/GetAll', null);
        };

        var getChatroomByName = function(chatroomName) {
            return chatroomRequester('GET', baseUrl + 'Chatroom/GetByName?name=' + chatroomName, null);
        };

        var getChatroomById = function(chatroomId) {
            return chatroomRequester('GET', baseUrl + 'Chatroom/GetById?id=' + chatroomId, null);
        };

        var joinChatroom = function(chatroomName) {
            return chatroomRequester('POST', baseUrl + 'Chatroom/Join?name=' + chatroomName, null);
        };

        var createChatroom = function(newChatroom) {
            return chatroomRequester('POST', baseUrl + 'Chatroom/create', 'Name=' + newChatroom);
        };


        return {
            getAllChatrooms: getAllChatrooms,
            getChatroomByName: getChatroomByName,
            getChatroomById: getChatroomById,
            joinChatroom: joinChatroom,
            createChatroom: createChatroom,
        };
    });