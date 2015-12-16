# Cake.AzureStorage

A Cake Addin for [Azure Storage](https://msdn.microsoft.com/en-us/library/azure/mt163683.aspx).

[![Build status](https://ci.appveyor.com/api/projects/status/1kphu06mh49fpw9e?svg=true)](https://ci.appveyor.com/project/RadioSystems/cake-azurestorage)

[![cakebuild.net](https://img.shields.io/badge/WWW-cakebuild.net-blue.svg)](http://cakebuild.net/)

[![Join the chat at https://gitter.im/cake-build/cake](https://badges.gitter.im/Join%20Chat.svg)](https://gitter.im/cake-build/cake?utm_source=badge&utm_medium=badge&utm_campaign=pr-badge&utm_content=badge)

## Functionality

Supports uploading Blobs to Azure Storage, more features to come shortly.

## Usage

To use the addin just add it to Cake call the aliases and configure any settings you want.

```csharp
#addin Cake.AzureStorage
...

// How to package with no settings
Task("PackageNoSettings")
	.Does(() => {
     var settings = new AzureStorageSettings();
     settings.AccountName = "AccountName";
     settings.Key = "API KEY";
     settings.ContainerName = "ContainerName";
     settings.BlobName = "BlobName";
	   UploadFileToBlog(settings, GetFile("./path/to/file/to/upload"));
	)};

```

Thats it.

Hope you enjoy using.
