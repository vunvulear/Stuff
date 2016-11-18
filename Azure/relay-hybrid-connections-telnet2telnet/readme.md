<b>What</b>
Client and server that can be used to connect two telnet clients between each other using relay hybrid connection. Comments can be found in the code that describes how client and server works. Both client and server requires a node.js to run.
The sample is using port 1235 for server telnet and port 1234 for client telnet. Ports can be changed, based on your needs.
No optimization are done of the code.
<br / >
<b>How to run:</b>
0. Update relay connection information
1. Start the server
2. Connect using telnet to server (from the same machine where server runs)
    telnet localhost 1235
3. Start the client
4. Connect using telnet to client (from the same machine where client runs)
    telnet localhost 1234
5. Write any content on client or server telnet. Content will be send to the other telnet instance.

PS: Even if the keys of relay are hardcoded, they are not the real one.
