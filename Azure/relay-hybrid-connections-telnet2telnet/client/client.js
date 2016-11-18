const net = require('net');
const webrelay = require('hyco-ws');

// Relay information
const ns = "rvrelay.servicebus.windows.net";
const path = "a";
const keyrule = "full";
const key = "hdm88wZHtAj412uqSV7IRscJiBKnFcWs3Sw5UFtWQ/o=";

// Local port used to send data
var sourceport = 1234;
var localsocket;
// Create relay connection for client
var relayclient = webrelay.relayedConnect(
        webrelay.createRelaySendUri(ns, path),
        webrelay.createRelayToken('http://'+ns, keyrule, key),
        function (socket) {
            console.log("Connected to relay")
            // Send msg to relay that confirms that cnnection was with success. 
            socket.send("Connection with success")

            // Define action for content received from relay
            relayclient.onmessage = function (event) {
                console.log('Data from relay: ' + event.data);
                // Send data to local socket (port)
                localsocket.write(event.data);
            };

            // Create local socket to the given port                     
            net.createServer(function(socket)
            {
                localsocket = socket;
                localsocket.on('data', function(d) {
                    // Send data from socket to 
                    relayclient.send(d);
                });
                }).listen(sourceport);            
        }
    ); 