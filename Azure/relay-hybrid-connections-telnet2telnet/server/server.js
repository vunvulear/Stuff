
const net = require('net');
const websocket = require('hyco-ws');


// Relay information
const ns = "rvrelay.servicebus.windows.net";
const path = "a";
const keyrule = "full";
const key = "hdm88wZHtAj412uqSV7IRscJiBKnFcWs3Sw5UFtWQ/o=";

// Local port where content is redirected (to/from)
var localport = 1235; 
var relaysocket;        
// Create local  socket connection
net.createServer(function(localsocket)
{
	// write to socket a dummy connection
	localsocket.write("Connection with success");
	localsocket.on('data', function(d) {
		// Receive data on local socket, redirect it to relay server (web socket) 		
		relaysocket.send(d);
	});

	// Create relay server, only after local socket was open
	var wss = websocket.createRelayedServer(
		{
			// Init listener to relay
			server : websocket.createRelayListenUri(ns, path),
			token: websocket.createRelayToken('http://' + ns, keyrule, key)
		}, 
		function (socket) {
			relaysocket = socket;
			console.log('New connection from client');
			relaysocket.onmessage = function (event) {
				// Send data to local socket (local port)
				localsocket.write(event.data);
				console.log("Send data to local port: " + event.data);
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

}).listen(localport);