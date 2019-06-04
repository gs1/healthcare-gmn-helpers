FROM ubuntu:bionic

ARG DEBIAN_FRONTEND=noninteractive

RUN apt-get update \
 && apt-get install -y --no-install-recommends apt-utils

RUN apt-get install -y software-properties-common apt-transport-https gnupg2 \
 && apt-get clean \
 && rm -r /var/lib/apt/lists/*

RUN apt-key adv --fetch-keys https://packages.microsoft.com/keys/microsoft.asc \
 && add-apt-repository -y "deb [arch=amd64] https://packages.microsoft.com/ubuntu/18.04/prod bionic main" \
 && apt-get update

RUN apt-key adv --keyserver hkp://keyserver.ubuntu.com:80 --recv-keys 3FA7E0328081BFF6A14DA29AA6A19B38D3D831EF \
 && add-apt-repository -y "deb https://download.mono-project.com/repo/ubuntu stable-bionic main" \
 && apt-get update

RUN apt-get install -y --no-install-recommends dotnet-sdk-2.2 mono-devel
RUN apt-get install -y --no-install-recommends default-jdk junit4
RUN apt-get install -y --no-install-recommends nodejs npm

RUN npm i -g jest-cli
RUN npm i -g jsdoc

COPY entrypoint.sh /entrypoint.sh

ENTRYPOINT ["/entrypoint.sh"]
CMD [""]

