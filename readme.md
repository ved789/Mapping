﻿##Steps to dockerise this app:

#1. Right click on project and add Docker support
#2. From csproj file remove the docker added texts as we shall be using cmd not visual studio
#3. Also delete both docker images: .net core and mapping that get created in step#1
#4. Now in powershell go to this project folder and run this command to create docker image:

docker build -f Dockerfile -t mappingapi .

#5. run this cmd to create container and run on port 5700

docker run -p 5700:80 --name mappingcontainer mappingapi:latest

#6. Now run this in browser:

http://localhost:5700/coordinateconvert

# post request json sample:

[{
    "fromMapPoints": [331524.552, 431910.792],
    "toMapPoints": null,
    "fromWkt": "coordsys 8,79,7,-2,49,0.9996012717,400000,-100000",
    "toWkt": "coordsys 2001,104,-180,-90,180,90"
},
{
    "fromMapPoints": [-3.0404548745705138, 53.77911026608691],
    "toMapPoints": null,
    "fromWkt": "mapinfo:coordsys 2001,104,-180,-90,180,90",
    "toWkt": "mapinfo:coordsys 8,79,7,-2,49,0.9996012717,400000,-100000"
}]


########################################################################
# Steps to install Gdal:
1. Add this path to the PATH environment system variable:
	C:\Confirm\Branches\GIS\Mapping\GDAL
2. Create three environment system variables:
	i)  GDAL_DATA = C:\Confirm\Branches\GIS\Mapping\GDAL\gdal-data
	ii) GDAL_DRIVER_PATH = C:\Confirm\Branches\GIS\Mapping\GDAL\gdalplugins\gdalplugins
	iii) PROJ_LIB = C:\Confirm\Branches\GIS\Mapping\GDAL\projlib
