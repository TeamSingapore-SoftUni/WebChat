webchatApp.factory('hubService',['$rootScope','Hub', '$timeout', 
    function($rootScope, Hub, $timeout){

    //declaring the hub connection
    var hub = new Hub('chathub', {

        //client side methods
        listeners:{
            'broadcastMessage': function (name, message) {
                console.log(name, message);
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
        console.log(hub);
        hub.Send(name, message); //Calling a server method
    };

    return {
        sendMessage: send
    };
}]);