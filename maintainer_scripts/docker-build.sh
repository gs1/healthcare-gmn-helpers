#! /bin/sh

set -e

IMAGE=docker-build

SCRIPTDIR=$(dirname $0)

cd $SCRIPTDIR
docker build . -t $IMAGE

cd ..
docker run -it --rm --mount type=bind,source="$(pwd)",target=/srv $IMAGE

