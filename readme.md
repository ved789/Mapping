##Steps to dockerise this app:

#1. Right click on project and add Docker support
#2. From csproj file remove the docker added texts as we shall be using cmd not visual studio
#3. Also delete both docker images: .net core and mapping that get created in step#1
#4. Now in powershell go to this project folder and run this command to create docker image:

docker build -f Dockerfile -t mappingapi .

#5. run this cmd to create container and run on port 5700

docker run -p 5700:80 --name mappingcontainer mappingapi:latest

#6. Now run this in browser:

http://localhost:5700/coordinateconvert

#post request json sample:

{
    "fromMapPoints": [331534.552, 431920.792],
    "toMapPoints": null,
    "fromWkt": "mapinfo:coordsys 8,79,7,-2,49,0.9996012717,400000,-100000",
    "toWkt": "mapinfo:coordsys 2001,104,-180,-90,180,90"
}