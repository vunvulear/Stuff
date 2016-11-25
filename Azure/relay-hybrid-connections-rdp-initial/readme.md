Initial script to create an RDP connection using Azure Relay Hybrid Connection. <br/>
The sample works to create the initial connection and validate credentials. <br/><br/>

The sample works until in the moment when RDP connection close the current socket connection and want to open multiple connection. At this step we shall add the logic to manage through the relay multiple connections. This is possible, but more works is required.

