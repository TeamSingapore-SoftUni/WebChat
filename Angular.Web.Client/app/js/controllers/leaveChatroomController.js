var webchatAppControllers = webchatAppControllers || angular.module('webchatAppControllers', []);

webchatApp.controller('LeaveChatroomModalController',
    function leaveChatroomModalController($scope, $rootScope, $modalInstance, $route, $location, chatroomName, chatroomService) {
        $scope.chatroomName = chatroomName;

        /* confirm chatroom leave*/
        $scope.ok = function() {
            chatroomService.leaveChatroom($scope.chatroomName ).then(function(data) {
                var message = {};
                message.Text = 'You have lleft #' + $scope.chatroomName ;
                message.Type = 'success';
                $rootScope.$broadcast('alertMessage', message);
                $location.path("/home");
            }, function(error) {
                $scope.errorOccurred = true;
                errorsService.handleError(error);
            });

            $modalInstance.close();
        };

        /* close modal dialog */
        $scope.cancel = function() {
            $modalInstance.dismiss('cancel');
            $route.reload();
        };
    });