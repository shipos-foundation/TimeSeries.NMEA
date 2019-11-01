#!/bin/bash
export VERSION=$(git tag --sort=-version:refname | head -1)
docker build --no-cache -f ./Source/Dockerfile -t shipos/timeseries-nmea . --build-arg CONFIGURATION="Release"
docker tag shipos/timeseries-nmea shipos/timeseries-nmea:$VERSION
docker push shipos/timeseries-nmea:latest
docker push shipos/timeseries-nmea:$VERSION
