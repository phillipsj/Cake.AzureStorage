# Cake.AzureStorage

A Cake Addin for [Azure Storage](https://msdn.microsoft.com/en-us/library/azure/mt163683.aspx).

[![Build status](https://ci.appveyor.com/api/projects/status/?svg=true)]()

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
		Squirrel(GetFile("Package.nupkg");
	)};

// How to package with the settings
Task("PackageWithSettings")
	.Does(() => {
		var settings = new SquirrelSettings();
		settings.NoMsi = true;
		settings.Silent = true;

		Squirrel(GetFile("Package.nupkg", settings);
	)};
```

Thats it.

Hope you enjoy using.
