webchatApp.factory('hubService',['$rootScope','Hub', '$timeout', '$filter', 
    function($rootScope, Hub, $timeout, $filter){

    //declaring the hub connection
    var hub = new Hub('messageHub', {

        //client side methods
        listeners:{
            'broadcastMessage': function (name, message, dateTime) {
                var formatedDateTime = $filter('date')(new Date(dateTime), 'dd-MMM /  HH:mm:ss');
                var messagesboxElement = $('#messagesbox');
                var messageElement = 
                    $('<div>[' + 
                    formatedDateTime + 
                    '] <strong>' + name + '</strong>: ' + 
                    message + 
                    '</div>').addClass('message');
                messagesboxElement.append(messageElement);
                messagesboxElement.scrollTop(messagesboxElement.prop('scrollHeight'));
                $rootScope.$apply();
            }
        },

        //server side methods
        methods: ['Send'],

        //query params sent on initial connection
        queryParams:{
            
        },

        //handle connection error
        errorHandler: function(error){
            console.error(error);
        },

        //specify a non default root
        //rootPath: '/api

        hubDisconnected: function () {                
            if (hub.connection.lastError) {
                hub.connection.start()
                    .done(function () {
                        if (hub.connection.state === 0) {
                            $timeout(function () { 
                                //your code here 
                            }, 2000);
                        } else {
                            //your code here
                        }
                    })
                    .fail(function (reason) {
                        //console.log(reason);
                    });
            }
        }
    });

    var send = function (name, message) {
        hub.Send(name, message); //Calling a server method
    };

    return {
        sendMessage: send
    };
}]);