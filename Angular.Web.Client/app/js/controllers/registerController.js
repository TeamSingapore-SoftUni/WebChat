var webchatAppControllers = webchatAppControllers || angular.module('webchatAppControllers', []);
/* login controller*/
webchatApp.controller('RegisterController',
    function registerController($scope, $rootScope, $location, authenticationService, authorizationService,
        errorsService) {
        var errorMessage;

        $rootScope.$broadcast('userLoginRegister');
        $scope.registrationActive = true;
        $scope.EMAIL_REGEXP = /^(([^<>()[\]\\.,;:\s@\"]+(\.[^<>()[\]\\.,;:\s@\"]+)*)|(\".+\"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/;

        $scope.register = function(credentials, registerForm) {
            if (registerForm.$valid) {
                authenticationService.register(credentials).then(function(data) {
                    authorizationService.setUserSession(data);
                    $rootScope.$broadcast('alertMessage', 'User account created.Please login');
                    //$location.path('/account/register');
                }, function(error) {
                    errorMessage = error.modelState;
                    errorsService.handleRegisterError(errorMessage);
                });
            }
        };
    });