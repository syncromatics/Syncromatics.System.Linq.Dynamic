#addin nuget:?package=Cake.Git&version=0.16.1
#addin nuget:?package=Cake.FileHelpers&version=2.0.0

#tool nuget:?package=NUnit.ConsoleRunner&version=3.4.0
#tool nuget:?package=GitVersion.CommandLine&version=4.0.0-beta0012

using System.Text.RegularExpressions;
using System.IO;

//////////////////////////////////////////////////////////////////////
// ARGUMENTS
//////////////////////////////////////////////////////////////////////

var target = Argument("target", "Default");
var configuration = Argument("configuration", "Release");

var version = "";
var semVersion = "";
var addLatest = false;

Task("GetVersion")
    .Does(() =>
    {
        var result = GitVersion(new GitVersionSettings {
            UpdateAssemblyInfo = false
        });

        semVersion = result.NuGetVersionV2;
        addLatest = result.BranchName == "master";

        Information($"##teamcity[buildNumber '{semVersion}']");

        Information($"SemVersion = '{semVersion}'");
    });

Task("Clean")
    .Does(() =>
    {
        CleanDirectories(GetDirectories("./Src/**/bin")
            .Concat(GetDirectories("./Src/**/obj")));
    });

Task("Restore")
    .IsDependentOn("Clean")
    .Does(() =>
    {
        Information("##teamcity[blockOpened name='Restore']");

        var settings = new DotNetCoreRestoreSettings
        {
            Sources = new[]
            {
                "https://www.nuget.org/api/v2/",
            },
            PackagesDirectory = Directory("packages")
        };

        DotNetCoreRestore("./Src/System.Linq.Dynamic.sln", settings);

        Information("##teamcity[blockClosed name='Restore']");
    });

Task("Build")
    .IsDependentOn("Restore")
    .IsDependentOn("GetVersion")
    .Does(() =>
    {
        var settings = new MSBuildSettings
        {
            Configuration = configuration,
            ArgumentCustomization = args => args.Append($"/property:Version={semVersion}"),
        };

        MSBuild("./Src/System.Linq.Dynamic.sln", settings);
    });

Task("UnitTests")
    .IsDependentOn("Build")
    .Does(() =>
    {
        var settings = new DotNetCoreTestSettings
        {
            Configuration = configuration,
            NoRestore = true,
            NoBuild = true,
        };
        DotNetCoreTest("./Src/System.Linq.Dynamic.Test", settings);
    });

Task("PackSystemLinqDynamic")
    .IsDependentOn("Build")
    .Does(() =>
    {
        Information("##teamcity[blockOpened name='PackSystemLinqDynamic']");

        var settings = new DotNetCorePackSettings
        {
            Configuration = configuration,
            ArgumentCustomization = args => args.Append($"/property:Version={semVersion}"),
            OutputDirectory = "./artifacts/",
            NoRestore = true,
            NoBuild = true
        };

        DotNetCorePack("./src/System.Linq.Dynamic/System.Linq.Dynamic.csproj", settings);

        Information("##teamcity[blockClosed name='PackSystemLinqDynamic']");
    });

Task("PushNugetPackages")
    .IsDependentOn("PackSystemLinqDynamic")
    .Does(() =>
    {
        Information("##teamcity[blockOpened name='PushNugetPackages']");

        var settings = new DotNetCoreNuGetPushSettings
        {
            Source = "https://www.myget.org/F/syncromatics/api/v2/package",
            ApiKey = EnvironmentVariable("MYGET_API_KEY")
        };

        DotNetCoreNuGetPush($"./artifacts/System.Linq.Dynamic.{semVersion}.nupkg", settings);

        Information("##teamcity[blockClosed name='PushNugetPackages']");
    });

Task("Test")
    .IsDependentOn("UnitTests");

Task("Package")
    .IsDependentOn("PackSystemLinqDynamic");

Task("Ship")
    .IsDependentOn("Test")
    .IsDependentOn("Package")
    .IsDependentOn("PushNugetPackages");

Task("Default")
    .IsDependentOn("Build");

RunTarget(target);
