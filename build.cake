#tool "nuget:?package=GitVersion.CommandLine"
//////////////////////////////////////////////////////////////////////
// ARGUMENTS
//////////////////////////////////////////////////////////////////////

var target = Argument("target", "Default");
var configuration = Argument("configuration", "Release");

//////////////////////////////////////////////////////////////////////
// PREPARATION
//////////////////////////////////////////////////////////////////////

// Define directories.
var buildDir = Directory("./Cake.AzureStorage/bin") + Directory(configuration) + Directory("netstandard1.6");
var artifacts = Directory("./artifacts");
//////////////////////////////////////////////////////////////////////
// TASKS
//////////////////////////////////////////////////////////////////////

Task("Clean")
    .Does(() =>
{
    CleanDirectory(buildDir);
	CleanDirectory(artifacts);
});

Task("Restore-NuGet-Packages")
    .IsDependentOn("Clean")
    .Does(() =>
{
    NuGetRestore("./Cake.AzureStorage.sln");
});

GitVersion versionInfo;
Task("GitVersion")
	.IsDependentOn("Restore-NuGet-Packages")
	.Does(() => 
{
	versionInfo = GitVersion(new GitVersionSettings {
		UpdateAssemblyInfo = true,
		OutputType = GitVersionOutput.Json
	});

	Information("GitVersion -> {0}", versionInfo.FullSemVer);
});

Task("Build")
	.IsDependentOn("GitVersion")
    .Does(() =>
{
	// Use MSBuild
	MSBuild("./Cake.AzureStorage.sln", settings =>
	settings.SetConfiguration(configuration));

	CopyFiles(buildDir.ToString() + "/*", artifacts);
});

Task("Run-Unit-Tests")
    .IsDependentOn("Build")
    .Does(() =>
{
    
});

Task("ILMerge")
    .IsDependentOn("Run-Unit-Tests")
    .Does(()=> {
        var assemblyPaths = GetFiles(buildDir.ToString() + "/*.dll");
        //ILMerge(buildDir.ToString() + "/Cake.AzureStorage.All.dll", buildDir.ToString() + "/Cake.AzureStorage.dll", assemblyPaths);
    });

Task("Create-NuGet-Package")
    .IsDependentOn("Run-Unit-Tests")
    .Does(() => 
{
    var nugetContent = new List<NuSpecContent>();
    foreach(var file in GetFiles(buildDir.ToString() + "/*")) {
		var content = new NuSpecContent {
			Target = "lib/netstandard1.6",
			Source = file.ToString()
		};

        nugetContent.Add(content);

		Information("{0} - {1}", content.Source, content.Target);
    }

    var settings = new NuGetPackSettings() {
        Id = "Cake.AzureStorage",
        Title = "Cake.AzureStorage",
        Authors = new [] { "Radio Systems Corporation" },
		Owners = new [] { "Radio Systems Corporation" },
        Copyright = "Copyright (c) 2016 Radio Systems Corporation",
        Description = "Cake Addin for working with Azure Storage",
        Tags = new [] {"azure", "storage", "cake"},
        ProjectUrl = new Uri("https://github.com/RadioSystems/Cake.AzureStorage"),
		LicenseUrl = new Uri("https://github.com/RadioSystems/Cake.AzureStorage/blob/master/LICENSE.md"),
        Version = versionInfo.NuGetVersion,
        Files = nugetContent,
        RequireLicenseAcceptance = false,
        OutputDirectory = artifacts
    };

    NuGetPack(settings);
});


//////////////////////////////////////////////////////////////////////
// TASK TARGETS
//////////////////////////////////////////////////////////////////////

Task("Default")
    .IsDependentOn("Create-NuGet-Package");

//////////////////////////////////////////////////////////////////////
// EXECUTION
//////////////////////////////////////////////////////////////////////

RunTarget(target);