/* Controllers */
var webchatAppControllers = webchatAppControllers || angular.module('webchatAppControllers', []);

webchatApp.controller('HomeController',
    function homeController($scope, $rootScope, $http, authorizationService, authenticationService,
        errorsService, userService, messageService, hubService, chatroomService) {
        $scope.foundChannel = {};
        $scope.searchChatroomClicked = false;
        $scope.chatroomFound = false;

        // get user info to display
        userService.getUserInfo().then(function(data) {
            $scope.userInfo = data;
        }, function(error) {
            errorsService.handleError(error);
        });

         // create a chatoom
        $scope.createChatroom = function(chatroomName) {
            chatroomService.createChatroom(chatroomName).then(function(data) {
                var message = {};
                message.Text = 'Chatroom ' +  chatroomName + ' created successfuly';
                message.Type = 'info';
                $rootScope.$broadcast('alertMessage', message);
            }, function(error) {              
                $scope.errorOccurred = true;
                errorsService.handleError(error);
            });
        };


        // search for a chatoom
        $scope.searchChatroom = function(chatroomName) {
            $scope.searchChatroomClicked = true;
            chatroomService.getChatroomByName(chatroomName).then(function(data) {
                $scope.foundChannel = data;
                $scope.chatroomFound = true;
            }, function(error) {              
                $scope.foundChannel = {
                    Name: "No chatrooms found.",
                }
            });
        };

        // join a chatoom
        $scope.joinChatroom = function(chatroomName) {
            chatroomService.joinChatroom(chatroomName).then(function(data) {
                $scope.searchChatroomClicked = false;
                $scope.chatroomFound = false;
                var message = {};
                message.Text = 'You have joined #' + chatroomName;
                message.Type = 'success';
                $rootScope.$broadcast('alertMessage', message);
            }, function(error) {              
                $scope.errorOccurred = true;
                errorsService.handleError(error);
            });
        };

        // Save message to database and push notification to SignalR.
        $scope.sendMessage = function(message) {
            if (message !== '') {
                messageService.sendToChatroom(message);
                clearInputField();
            }
        };

        $scope.listMessagesInChatroom = function() {
            messageService.getMessagesFromChatroom("ba18a49e-605c-4781-8a3e-597a79d8068b")
                .then(function(data) {
                    $scope.chat = data;
                });
        }

        $scope.$on('messagesRendered', function(messagesRenderedEvent) {
            var messagesbox = document.getElementById('messagesbox');
            messagesbox.scrollTop = messagesbox.scrollHeight;
        });

        function clearInputField() {
            var inputField = document.getElementById('input-messagebox').firstElementChild;
            inputField.value = '';
        }
    });