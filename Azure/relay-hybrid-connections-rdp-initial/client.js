const net = require('net');
const webrelay = require('hyco-ws');

// Relay information
const ns = "rvrelay.servicebus.windows.net";
const path = "a";
const keyrule = "full";
const key = "ja+eEVe8CbW04EahE+lljhXQn+XmFPi3BeGFLe1go/k=";

// Local port used to send data
var sourceport = 3389;
var localsocket;
// Create relay connection for client
var relayclient = webrelay.relayedConnect(
        webrelay.createRelaySendUri(ns, path),
        webrelay.createRelayToken('http://'+ns, keyrule, key),
        function (socket) {
            // Create local socket to the given port                               
            console.log("Connected to relay")
            relayclient.onmessage = function (event) {
                if(typeof localsocket === "undefined")
                {
                    localsocket = net.connect(sourceport,function(socket)  {
                            console.log("New socket");
                        });
                    localsocket.on('data', function(data) {
                        relayclient.send(data);
                    });
                    localsocket.on('error', function(err) {console.log("Socket close: " + err.stack)});
                }
                localsocket.write(event.data);
            };            
        }
    ); 
