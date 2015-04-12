/* Controllers */
var webchatAppControllers = webchatAppControllers || angular.module('webchatAppControllers', []);

webchatApp.controller('IndexHtmlController',
	function mainController($scope, $rootScope, $window, $location, $timeout, authorizationService, authenticationService, errorsService, ajaxErrorText) {
		$scope.userHasLogged = false;
		$scope.currentUser  = authorizationService.getUsername();
		
		/* handle alert messages */
		$scope.alertDialog = false;
		$scope.alertMsg = '';
		$scope.alertType = '';

		$scope.closeAlert = function() {
			$scope.alertDialog = false;
			$('.alerts-div').css('z-index', -1);
		};

		/* This event is sent by all controllers after success/error ajax callback */
		$scope.$on('alertMessage', function(event, message) {
			$scope.alertDialog = true;
			$scope.alertMsg = message;
			$scope.alertType = 'danger';
			$('.alerts-div').css('z-index', 99);

			/* autohide alert message */
			$timeout(function() {
				$("#current-alert").fadeTo(500, 0).slideUp(500, function() {
					$scope.alertDialog = false;
					$('.alerts-div').css('z-index', -1);
				});
			}, 5000);
		});

		/* This event is sent by mainController whe user has loged*/
		$scope.$on('userHasLogged', function(event, message) {
			$scope.userHasLogged = true;
		});

		/* handle refreshing page to store services state and user data */
		function init() {
			if (authorizationService.userIsLogged()) {
				$scope.userHasLogged = true;
				$scope.currentUser = authorizationService.getUsername();
				$location.path('/home')
			}
        }

        init();

		
		/* logout an alet user*/
		$scope.logout = function() {
			$scope.currentUser  = authorizationService.getUsername();
			authenticationService.logout();
			$scope.userHasLogged = false;
			$location.path('/');
			
			/* alert user */
			$scope.alertDialog = true;
			$scope.alertMsg = 'Goodbye ' + $scope.currentUser + '.Thank you for using our services!';
			$scope.alertType = 'danger';
			$('.alerts-div').css('z-index', 99);

			/* autohide alert message */
			$timeout(function() {
				$("#current-alert").fadeTo(500, 0).slideUp(500, function() {
					$scope.alertDialog = false;
					$('.alerts-div').css('z-index', -1);
				});
			}, 5000);
		};
	});