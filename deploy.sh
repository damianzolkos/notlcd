#!/bin/bash

CONTAINER_NAME="notlcd"
IMAGE_NAME="notlcd"

# Check if the container exists
if [ "$(docker ps -a -q -f name=$CONTAINER_NAME)" ]; then
    echo "Stopping and removing existing container: $CONTAINER_NAME"
    # Stop and remove the container if it exists
    docker stop $CONTAINER_NAME
    docker rm $CONTAINER_NAME
fi

dotnet publish -c Release
docker build -t $IMAGE_NAME -f Dockerfile .
docker create --name $CONTAINER_NAME $IMAGE_NAME
docker run -d --name $CONTAINER_NAME -p 8080:8080 $IMAGE_NAME