
const net = require('net');
const websocket = require('hyco-ws');


// Relay information
const ns = "rvrelay.servicebus.windows.net";
const path = "a";
const keyrule = "full";
const key = "ja+eEVe8CbW04EahE+lljhXQn+XmFPi3BeGFLe1go/k=";

// Local port where content is redirected (to/from)
var localport = 3333; 
var relaysocket; 
var myLocalHost;     

var wss = websocket.createRelayedServer({
		// Init listener to relay
		server : websocket.createRelayListenUri(ns, path),
		token: websocket.createRelayToken('http://' + ns, keyrule, key)
	}, 
	function (socket) {
		relaysocket = socket;
		console.log('New connection from client');
		relaysocket.onmessage = function (event) {
			// Send data to local socket (local port)
			myLocalHost.write(event.data);
		};
		relaysocket.on('close', function () {
			console.log('Relay connectin was closed');
		});       
	});

	console.log('Ready for new connection');
	wss.on('error', function(err) {
		console.log('error' + err);
	});

	websocket.createRelayListenUri() 


// Create local  socket connection
net.createServer(function(localsocket)
{
	myLocalHost = localsocket;
	myLocalHost.on('data', function(d) {
		relaysocket.send(d);
		myLocalHost.on('error', function(err) {console.log("Socket close: " + err.stack)});
	});
}).listen(localport);
