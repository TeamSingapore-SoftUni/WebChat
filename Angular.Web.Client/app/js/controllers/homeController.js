/* Controllers */
var webchatAppControllers = webchatAppControllers || angular.module('webchatAppControllers', []);

webchatApp.controller('HomeController',
    function homeController($scope, $rootScope, $http, authorizationService, authenticationService, errorsService, userService, messageService, hubService) {
        // get user info to display
        userService.getUserInfo().then(function(data) {
            $scope.userInfo = data;
        }, function(error) {
            errorsService.handleError(error);
        });
        // Save message to database and push notification to SignalR.
        $scope.sendMessage = function(message) {
            if (message !== '') {
                messageService.sendToChatroom(message);
                clearInputField();
            }
        };

        $scope.listMessagesInChatroom = function() {    
            messageService.getMessagesFromChatroom("F0C58BCC-93C7-4663-8709-180205CA2F19")
            .then(function (data) {
                $scope.chat = data;
            });
        };

        $scope.$on('messagesRendered', function(messagesRenderedEvent) {
            var messagesbox = document.getElementById('messagesbox');
            messagesbox.scrollTop = messagesbox.scrollHeight;
        });

        function clearInputField() {
            var inputField = document.getElementById('input-messagebox').firstElementChild;
            inputField.value = '';
        }
    });