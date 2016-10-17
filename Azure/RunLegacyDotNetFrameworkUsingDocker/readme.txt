The main purpose of this sample is to show how we can run Legacy applications using Docker and Windows Server 2016

Build docker image:
docker build -f Dockerfile.txt -t legacy .

Run docker image:
docker run legacy

Inspect image:
docker inspect legacy

Remove docker image legacy:
docker rmi legacy --force

Remove docker container legacy
docker rm legacy

Remove all containers
docker rm $(docker ps -a -q)

docker build -f Dockerfile.txt -t legacy .
docker run legacy
