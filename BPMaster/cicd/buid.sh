#!/bin/bash
sudo docker build -t $image_name:$tag -f Dockerfile .
sudo docker images
sudo docker push $image_name:$tag