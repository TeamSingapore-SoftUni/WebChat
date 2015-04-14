'use strict';

webchatApp
    .directive('leftSidebar', function() {
       return {
           controller: 'HomeController',
           restrict: 'E',
           templateUrl: 'templates/leftSidebar.html',
           replace: true
       }
    });