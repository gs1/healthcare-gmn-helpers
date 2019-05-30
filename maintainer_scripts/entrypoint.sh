#!/bin/sh

# Build C#
cd /srv/cs
dotnet build GS1GMN.sln
dotnet test GS1GMN.sln
dotnet pack -c Release -o app HealthcareGMN/HealthcareGMN.csproj
cp HealthcareGMN/app/HealthcareGMN.*.nupkg .

# Build Java
cd /srv/java
javac -encoding UTF-8 org/gs1/HealthcareGMN.java
javac -encoding UTF-8 -cp .:/usr/share/java/junit4.jar HealthcareGMNTests.java
java -cp .:/usr/share/java/junit4.jar org.junit.runner.JUnitCore HealthcareGMNTests
jar -cvf HealthcareGMN.jar org/gs1/*.class
javadoc -d docs org.gs1

# Build JS
cd /srv/js
jest
jsdoc -d doc healthcaregmn.js

