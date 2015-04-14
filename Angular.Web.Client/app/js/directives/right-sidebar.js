'use strict';

webchatApp
    .directive('rightSidebar', function() {
       return {
           controller: 'HomeController',
           restrict: 'E',
           templateUrl: 'templates/rightSidebar.html',
           replace: true
       }
    });