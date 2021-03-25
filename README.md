# Cake.AzureStorage

A Cake Addin for [Azure Storage](https://msdn.microsoft.com/en-us/library/azure/mt163683.aspx).

[![Build status](https://ci.appveyor.com/api/projects/status/1kphu06mh49fpw9e?svg=true)](https://ci.appveyor.com/project/RadioSystems/cake-azurestorage)
[![Develop MyGet Build Status](https://www.myget.org/BuildSource/Badge/cake-azure-storage?identifier=8bc9fc41-15fa-49bb-9872-82f198fa7566)](https://www.myget.org/)
[![cakebuild.net](https://img.shields.io/badge/WWW-cakebuild.net-blue.svg)](http://cakebuild.net/)
[![Join in the discussion on the Cake repository](https://img.shields.io/badge/GitHub-Discussions-green?logo=github)](https://github.com/cake-build/cake/discussions)

## Functionality

Supports uploading Blobs to Azure Blob Storage as well as deleting them by prefix and by name. More features to be added in the future.

## Usage

To use the addin just add it to Cake call the aliases and configure any settings you want.

```csharp
#addin "Cake.AzureStorage"
...

// How to package with no settings
Task("PackageNoSettings")
    .Does(() => {
     var settings = new AzureStorageSettings();
     settings.AccountName = "AccountName";
     settings.Key = "API KEY";
     settings.ContainerName = "ContainerName";
     settings.BlobName = "BlobName";
       UploadFileToBlob(settings, GetFile("./path/to/file/to/upload"));
    });

// How to specify blob content type if necessary
Task("PackageSpecifyContentType")
    .Does(() => {
     var settings = new AzureStorageSettings();
     settings.AccountName = "AccountName";
     settings.Key = "API KEY";
     settings.ContainerName = "ContainerName";
     settings.BlobName = "BlobName";
     settings.ContentType = "image/jpeg";
       UploadFileToBlob(settings, GetFile("./path/to/file/to/upload"));
    });



Task("PackageAfterDelete")
    .Does(() => {
        var settings = new AzureStorageSettings();
        settings.AccountName = "AccountName";
        settings.Key = "API KEY";
        settings.ContainerName = "ContainerName";
        settings.BlobName = "NameToDelete";
        DeleteBlob(settings);

        settings.BlobName = "NameToUploadAs";
        var filePath = new FilePath("./location/of/file/to/upload");
        UploadFileToBlob(settings, filePath);
    });

Task("PackageAfterMultiDelete")
    .Does(() => {
        var settings = new AzureStorageSettings();
        settings.AccountName = "AccountName";
        settings.Key = "API KEY";
        settings.ContainerName = "ContainerName";
        settings.BlobName = "PrefixToSearchBy";

        var deletedLog = DeleteBlobsByPrefix(settings);
        foreach (var line in deletedLog){
            Information(line);
        }

        settings.BlobName = "NameToUploadAs";
        var filePath = new FilePath("./location/of/file/to/upload");
        UploadFileToBlob(settings, filePath);
    });
```

The Prefix used for deleting blobs is exactly like it sounds, a prefix of the file you want to delete. So if you have files named ABC-ImportantFile-1.0.0.1.exe, ABC-ImportantFile-1.0.0.2.exe, ABC-ImportantFile-1.0.0.3.exe, etc. You can delete these by specifying "ABC-ImportantFile-" as your blob name and it will get all files starting with that string. 

That's it.

Hope you enjoy using.

## Discussion

For questions and to discuss ideas & feature requests, use the [GitHub discussions on the Cake GitHub repository](https://github.com/cake-build/cake/discussions), under the [Extension Q&A](https://github.com/cake-build/cake/discussions/categories/extension-q-a) category.

[![Join in the discussion on the Cake repository](https://img.shields.io/badge/GitHub-Discussions-green?logo=github)](https://github.com/cake-build/cake/discussions)
