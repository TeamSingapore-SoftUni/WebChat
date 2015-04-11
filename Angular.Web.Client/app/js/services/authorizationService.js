webchatApp.factory('authorizationService', function authorizationService($window) {
    var headers = {};
    var userSession;

    function setUserSession(data) {
        userSession = {
            accessToken: data.access_token,
            userName: data.username
        };

        $window.sessionStorage["currentUser"] = JSON.stringify(userSession);
    }

    function getCurrentUser() {
        var userData = sessionStorage['currentUser'];
        if (userData) {
            return JSON.parse(sessionStorage['currentUser']);
        }
    }

    function getUsername() {
        var userData = sessionStorage['currentUser'];
        if (userData) {
            var userObject = JSON.parse(sessionStorage['currentUser']);
            return userObject.userName;
        }
    }

    function getAccessToken() {
        var userData = sessionStorage['currentUser'];
        if (userData) {
            var userObject = JSON.parse(sessionStorage['currentUser']);
            return userObject.accessToken;
        }
    }

    function userIsLogged() {
        var userData = sessionStorage['currentUser'];
        if (userData) {
            return true;
        }

        return false;
    }

    function getAuthorizationHeaders() {
        var accessToken = getAccessToken();
        if (accessToken) {
            angular.extend(headers, {
                Authorization: 'Bearer ' + accessToken
            });
            return headers;
        }

        return null;
    }

    
    function deleteAuthorizationHeaders() {
        delete headers['Authorization'];
    }

    return {
        setUserSession: setUserSession,
        getCurrentUser: getCurrentUser,
        userIsLogged: userIsLogged,
        getUsername: getUsername,
        getAccessToken: getAccessToken,
        getAuthorizationHeaders: getAuthorizationHeaders,
        deleteAuthorizationHeaders: deleteAuthorizationHeaders
    };
});