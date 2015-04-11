/* App Module */
var webchatApp = angular.module('webchatApp', [
    'ngResource',
    'ngSanitize',
    'ngRoute',
    'ui.bootstrap',
    'angularUtils.directives.dirPagination',
    'webchatAppControllers',
]);

/* Configure routing and URL paths */
webchatApp.config(['$routeProvider',
    function($routeProvider, $locationProvider) {
        $routeProvider.
        when('/login', {
            templateUrl: 'templates/login.html',
            controller: 'LoginController'
        }).
        when('/register', {
            templateUrl: 'templates/register.html',
            controller: 'RegisterController'
        }).
        when('/home', {
            templateUrl: 'templates/home.html',
            controller: 'HomeController'
        }).    
        when('/unauthorized', {
            templateUrl: 'templates/unauthorized.html'
        }).
        otherwise({
            redirectTo: '/'
        });
    }
]).
run(function($rootScope, $location, authorizationService) {
    $rootScope.$on('$routeChangeStart', function(event, next) {
        var path = $location.path();
        if (!authorizationService.userIsLogged() && path !== '/login' && path !== '/register' && path !== '/home') {
            $location.path('/unauthorized');
        };
    });
}).
constant('baseUrl', 'http://localhost:5789/api/')
    .constant('ajaxErrorText', 'Something went wrong, please try again or refresh the page.');
