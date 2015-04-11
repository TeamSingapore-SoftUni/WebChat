webchatApp.factory('authenticationService',
    function authentication($http, $q, baseUrl, authorizationService) {
        function authenticateRequester(method, url, data) {
            var deferred = $q.defer();

            $http({
                method: method,
                url: url,
                data: data,
            })
                .success(function(data, status, headers, config) {
                    deferred.resolve(data, status, headers, config);
                })
                .error(function(data, status, headers, config) {
                    deferred.reject(data, status, headers, config);
                });

            return deferred.promise;
        }

        var login = function(data) {
            return authenticateRequester('POST', baseUrl + 'Account/login', "userName=" + data.username + 
                "&password=" + data.password +  "&grant_type=password");
        };

        var register = function(credentials) {
            return authenticateRequester('POST', baseUrl + 'Account/register', credentials);
        };

        function logout() {
            var deferred = $q.defer(),
                headers = authorizationService.getAuthorizationHeaders();

            $http({
                method: 'POST',
                url: baseUrl + 'Account/Logout',
                data: {},
                headers: headers
            })
                .success(function(data, status, headers, config) {
                    authorizationService.deleteAuthorizationHeaders();
                    delete sessionStorage['currentUser'];
                    deferred.resolve(data, status, headers, config);
                })
                .error(function(data, status, headers, config) {
                    deferred.reject(data, status, headers, config);
                });

            return deferred.promise;
        }

        return {
            login: login,
            register: register,
            logout: logout
        };
    });