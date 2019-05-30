#! /bin/sh

IMAGE=docker-build

SCRIPTDIR=$(dirname $0)

cd $SCRIPTDIR
docker build . -t $IMAGE

cd ..
docker run -it --mount type=bind,source="$(pwd)",target=/srv $IMAGE

