#tool "nuget:?package=ilmerge"
//////////////////////////////////////////////////////////////////////
// ARGUMENTS
//////////////////////////////////////////////////////////////////////

var target = Argument("target", "Default");
var configuration = Argument("configuration", "Release");

//////////////////////////////////////////////////////////////////////
// PREPARATION
//////////////////////////////////////////////////////////////////////

// Define directories.
var buildDir = Directory("./Cake.AzureStorage/bin") + Directory(configuration);
var assemblyInfo = ParseAssemblyInfo("./Cake.AzureStorage/Properties/AssemblyInfo.cs");
//////////////////////////////////////////////////////////////////////
// TASKS
//////////////////////////////////////////////////////////////////////

Task("Clean")
    .Does(() =>
{
    CleanDirectory(buildDir);
});

Task("Restore-NuGet-Packages")
    .IsDependentOn("Clean")
    .Does(() =>
{
    NuGetRestore("./Cake.AzureStorage.sln");
});

Task("Build")
    .IsDependentOn("Restore-NuGet-Packages")
    .Does(() =>
{
      // Use MSBuild
      MSBuild("./Cake.AzureStorage.sln", settings =>
        settings.SetConfiguration(configuration));
});

Task("Run-Unit-Tests")
    .IsDependentOn("Build")
    .Does(() =>
{
    
});

Task("Create-NuGet-Package")
    .IsDependentOn("Run-Unit-Tests")
    .Does(()=> {
        var versionNumber = assemblyInfo.AssemblyVersion;
        if(MyGet.IsRunningOnMyGet) {
            versionNumber = versionNumber + "-beta";
        }
        var settings = new NuGetPackSettings(){
            Version = versionNumber,
            BasePath = buildDir.ToString()           
        };
        NuGetPack("./Cake.AzureStorage/Cake.AzureStorage.nuspec", settings);
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