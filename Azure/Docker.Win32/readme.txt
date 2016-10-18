This samples covers two use cases for .NET Framework and Docker:
  - Win32 calls
  - COM calls
  
How to run:
  Create the docker image:
    docker build -f Docker.Win32.Image.txt -t win32 . 
  Run the container with the above image:
    docker run win32
    
More details related to Docker:
  https://github.com/vunvulear/Stuff/tree/master/Azure/RunLegacyDotNetFrameworkUsingDocker
