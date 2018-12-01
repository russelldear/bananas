#!/bin/bash
dotnet build
dotnet lambda package
aws lambda update-function-code --function-name bnaas --publish --zip-file fileb://bin/Release/netcoreapp2.1/bnaas.zip

#run chmod +x deploy.sh first if you have permissions issues