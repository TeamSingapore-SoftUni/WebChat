/* App Module */
var webchatApp = angular.module('webchatApp', [
	'ngResource',
	'ngSanitize',
	'ngRoute',
	'ui.bootstrap',
	'angularUtils.directives.dirPagination',
	'webchatAppControllers'
	]);

/* Configure routing and URL paths */
webchatApp.config(['$routeProvider',
	function($routeProvider, $locationProvider) {
		$routeProvider.
		when('/', {
			templateUrl: 'templates/main.html',
			controller: 'MainController'
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
	])
.run(function($rootScope, $location, authorizationService) {
	$rootScope.$on('$routeChangeStart', function(event, next) {
		var path = $location.path();
		if (!authorizationService.userIsLogged() && path !== '/') {
			$location.path('/unauthorized');
		}
	});
})
.constant('baseUrl', 'http://localhost:5789/api/')
.constant('ajaxErrorText', 'Something went wrong, please try again or refresh the page.');