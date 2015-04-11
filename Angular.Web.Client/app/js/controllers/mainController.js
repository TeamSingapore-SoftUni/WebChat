/* Controllers */
var webchatAppControllers = webchatAppControllers || angular.module('webchatAppControllers', []);

webchatApp.controller('MainController',
    function mainController($scope, $rootScope, $window, $location, $timeout, authorizationService, authenticationService, errorsService) {
        // login
        $scope.login = function(credentials, loginForm) {
            if (loginForm.$valid) {
                authenticationService.login(credentials).then(function(data) {
                    authorizationService.setUserSession(data);
                    /* set an eventHandler on rootScope for user logging */
                    $rootScope.$broadcast('userHasLogged');
                    $location.path("/home");
                }, function(error) {
                    $scope.errorOccurred = true;
                    errorsService.handleLogingError(error);
                });
            }

        };
        // register
        $scope.register = function(credentials, registerForm) {
            if (registerForm.$valid) {
                credentials.ImageDataURL = null;
                authenticationService.register(credentials).then(function(data) {
                    authorizationService.setUserSession(data);
                    $rootScope.$broadcast('alertMessage', 'User account created.Please login');
                }, function(error) {
                    var errorMessage = error.ModelState;
                    errorsService.handleRegisterError(errorMessage);
                });
            }
        };
    });