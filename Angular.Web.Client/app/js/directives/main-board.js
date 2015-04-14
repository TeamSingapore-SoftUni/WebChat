'use strict';

webchatApp
    .directive('mainBoard', function() {
        return {
            controller: 'HomeController',
            restrict: 'E',
            templateUrl: 'templates/mainBoard.html',
            replace: true
        }
    });