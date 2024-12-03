#!/bin/bash
cd /home/ec2-user/rpms-service
docker-compose build --no-cache
docker-compose up -d
