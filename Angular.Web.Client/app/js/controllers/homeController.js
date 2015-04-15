/* Controllers */
var webchatAppControllers = webchatAppControllers || angular.module('webchatAppControllers', []);

webchatApp.controller('HomeController',
    function homeController($scope, $rootScope, $route, $location, $http, authorizationService, authenticationService,
        errorsService, userService, messageService, hubService, chatroomService) {
        // this is the current chatroom(private chat) id and name to which signalR will send/reciev messages
        getCurrentChatroomId();
        getCurrentChatroomName();

        $scope.foundChannel = {};
        $scope.searchChatroomClicked = false;
        $scope.chatroomFound = false;

        $scope.foundUser = {};
        $scope.searchUserClicked = false;
        $scope.userFound = false;

        $scope.noChatroomsToDisplay = false;

        // get user info to display
        userService.getUserInfo().then(function(data) {
            $scope.userInfo = data;
        }, function(error) {
            errorsService.handleError(error);
        });

        // get user chatrooms to display
        userService.getUserChatrooms().then(function(data) {
            if (data.length == 0) {
                $scope.noChatroomsToDisplay = true;
            };

            $scope.userChatrooms = data;
        }, function(error) {
            errorsService.handleError(error);
        });

        // load clicked chatroom and list all users in it 
        $scope.loadChatroom = function(chatroomId, chatroomName) {
            $location.path("/home/chatroom/" + chatroomId);
            $scope.currentChatroomName = chatroomName;
        };

        //get users in chatoom
       // getUsersInChatroom();

        // create a chatoom
        $scope.createChatroom = function(chatroomName) {
            if (!$scope.newChatroomName) {
                var message = {};
                message.Text = "Chatroom name can not be empty!"
                message.Type = 'danger'
                $rootScope.$broadcast('alertMessage', message);
                return;
            };

            chatroomService.createChatroom(chatroomName).then(function(data) {
                var message = {};
                message.Text = 'Chatroom #' + chatroomName + ' created successfuly';
                message.Type = 'info';
                $rootScope.$broadcast('alertMessage', message);

                //reload page and load created chatroom
                $location.path("/home/chatroom/" + data.chatroom.Id);
                $scope.currentChatroomName = data.chatroom.Name;
            }, function(error) {
                $scope.errorOccurred = true;
                errorsService.handleError(error);
            });
        };


        // search for a chatoom
        $scope.searchChatroom = function(chatroomName) {
            $scope.chatroomFound = false;
            if (!$scope.chatroomName) {
                return;
            };

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

                //reload the page and load joined chatroom
                $location.path("/home/chatroom/" + data.ChatoomId);
                $scope.currentChatroomName = data.ChatroomName;
            }, function(error) {
                $scope.errorOccurred = true;
                errorsService.handleError(error);
            });
        };

        // search for a user
        $scope.searchUser = function(searchUserName) {
            $scope.userFound = false;
            if (!$scope.searchUserName) {
                return;
            };

            $scope.searchUserClicked = true;
            userService.getUserByName(searchUserName).then(function(data) {
                $scope.foundUser = data;
                $scope.userFound = true;
            }, function(error) {
                $scope.foundUser = {
                    UserName: "No users found.",
                }
            });
        };

        // search for a user
        // not imlemented
        $scope.chatWithuser = function(userId) {
            alert(userId);
            // userService.getUserByName(searchUserName).then(function(data) {
            //     $scope.foundUser = data;
            //     $scope.userFound = true;
            // }, function(error) {              
            //     $scope.foundUser = {
            //         Name: "No users found.",
            //     }
            // });
        };


        // Save message to database and push notification to SignalR.
        $scope.sendMessage = function(message) {
            // pass the currentchatroomID to the messigeService sender
            var currentChatroom = $scope.currentChatroomID;
            var path = $location.path();
            if (path !== '/home') {
                currentChatroom = $location.path().substr(15);
            };

            if (message !== '') {
                messageService.sendToChatroom(message, currentChatroom);
                clearInputField();
            }
        };

        $scope.listMessagesInChatroom = function() {
            // pass the currentchatroomID to the messigeService receiver
            var currentChatroom = $scope.currentChatroomID;
            var path = $location.path();
            if (path !== '/home') {
                currentChatroom = $location.path().substr(15);
            };

            messageService.getMessagesFromChatroom(currentChatroom)
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

        // escape user input
        function htmlEntities(str) {
            return String(str).replace(/&/g, '&amp;').replace(/</g, '&lt;').replace(/>/g, '&gt;').replace(/"/g, '&quot;');
        }

        //get currrentchatroomId
        function getCurrentChatroomId() {
            var path = $location.path();
            if (path !== '/home') {
                $scope.currentChatroomID = $location.path().substr(15);
            } else {
                $scope.currentChatroomID = "ba18a49e-605c-4781-8a3e-597a79d8068b";
            }
        }

        //get currrentchatroomName 
        function getCurrentChatroomName() {
            var path = $location.path();
            if (path !== '/home') {
                var chatroomId = $location.path().substr(15);
                chatroomService.getChatroomById(chatroomId).then(function(data) {
                    $scope.currentChatroomName = data.Name;
                }, function(error) {
                    $scope.errorOccurred = true;
                    errorsService.handleError(error);
                });
            } else {
                $scope.currentChatroomName = "Default chatoom."
            }
        }

        // function getUsersInChatroom() {
        //     var currentChatroomName = $scope.currentChatroomName;
        //     if (currentChatroomName !== "Default chatoom.") {
        //         chatroomService.getUsersInChatroom(currentChatroomName).then(function(data) {
        //             if (data.Users.length == 0) {
        //                 $scope.noUsersInChatroom = true;
        //             };

        //             $scope.usersInChatroom = data.Users;
        //             $scope.usersInChatroomName = data.Name;
        //         }, function(error) {
        //             $scope.errorOccurred = true;
        //             //   errorsService.handleError(error);
        //         });
        //     };
        // }


        /* activate clicked links on page refresh*/
        $scope.getClass = function(path) {
            if ($location.path().substr(15) === path) {
                return "active";
            } else {
                return "";
            }
        };

    });