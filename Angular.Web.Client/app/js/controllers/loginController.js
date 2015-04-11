var webchatAppControllers = webchatAppControllers || angular.module('webchatAppControllers', []);
/* login controller*/
webchatApp.controller('LoginController',
    function loginController($scope, $rootScope, $location, authenticationService, authorizationService) {
        $rootScope.$broadcast('userLoginRegister');

        $scope.login = function(credentials, loginForm) {
            credentials.grant_type = "password";
            if (loginForm.$valid) {
                authenticationService.login(credentials)
                    .then(function(data) {
                        authorizationService.setUserSession(data);
                        /* set an eventHandler on rootScope for user logging */
                        $rootScope.$broadcast('userHasLogged');
                    },
                    function(error) {
                        $scope.errorOccurred = true;
                        //errorsService.handleLogingError(error);
                    });
            }
        };
    });