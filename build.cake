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
    .Does(()=> {
        var settings = new NuGetPackSettings(){
            Version = versionInfo.NuGetVersion,
            BasePath = buildDir.ToString()           
        };
        NuGetPack("./Cake.AzureStorage.nuspec", settings);
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