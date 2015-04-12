/* Controllers */
var webchatAppControllers = webchatAppControllers || angular.module('webchatAppControllers', []);

webchatApp.controller('HomeController',
    function homeController($scope, $rootScope, $http, authorizationService, authenticationService, errorsService, userService) {
        // get user info to display
        userService.getUserInfo().then(function(data) {
            $scope.userInfo = data;
        }, function(error) {
            errorsService.handleError(error);
        });

    });