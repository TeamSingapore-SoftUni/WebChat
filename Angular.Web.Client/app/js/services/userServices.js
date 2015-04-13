webchatApp.factory('userService', function($http, $q, baseUrl, authorizationService) {
    function userRequester(method, url, data) {
        var deferred = $q.defer();
        var headers = authorizationService.getAuthorizationHeaders();

        $http({
            method: method,
            url: url,
            headers: headers,
            data: data
        })
            .success(function(data, status, headers, config) {
                deferred.resolve(data, status, headers, config);
            })
            .error(function(data, status, headers, config) {
                deferred.reject(data, status, headers, config);
            });

        return deferred.promise;
    }

    var getUserInfo = function() {
        return userRequester('GET', baseUrl + '/Account/UserInfo', null);
    };


    return {
        getUserInfo: getUserInfo,

    };
});