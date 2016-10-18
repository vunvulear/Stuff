Sample code to validate if we can run a Web Form app in a Docker container

Usefull commands:
  Build Docker image:
    docker build -f Docker.WebForms.txt -t webforms .
  Run Docker container with our image:
    docker run -d --name webforms webforms
  Get container IP that needes to be used in the browser:
    docker inspect -f "{{ .NetworkSettings.Networks.nat.IPAddress }}" webforms
  Stop container with our web form app:
    docker stop webforms
  
  
