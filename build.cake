// Install tools
#tool "nuget:?package=GitVersion.CommandLine&version=3.5.4"

// Load scripts
#load "./build/parameters.cake"

///////////////////////////////////////////////////////////////////////////////
// PARAMETERS
///////////////////////////////////////////////////////////////////////////////

var parameters = BuildParameters.GetParameters(Context);

///////////////////////////////////////////////////////////////////////////////
// SETUP
///////////////////////////////////////////////////////////////////////////////

Setup(context => {
    parameters.SetBuildVersion(
        BuildVersion.CalculatingSemanticVersion(
            context: Context,
            parameters: parameters
        )
    );

    parameters.SetBuildPaths(
        BuildPaths.GetPaths(
            context: Context,
            configuration: parameters.Configuration,
            semVersion: parameters.Version.SemVersion
        )
    );

    Information("Building version {0} of Cake.ServiceFabric ({1}, {2}).",
        parameters.Version.SemVersion,
        parameters.Configuration,
        parameters.Target);
});


///////////////////////////////////////////////////////////////////////////////
// TASKS
///////////////////////////////////////////////////////////////////////////////

Task("Clean")
    .Does(() =>
{
    CleanDirectories(parameters.Paths.Directories.ToClean);
});

Task("Restore-NuGet-Packages")
    .IsDependentOn("Clean")
    .Does(() =>
{
    NuGetRestore(parameters.Paths.Files.Solution, new NuGetRestoreSettings {
        Source = new List<string> {
            "https://api.nuget.org/v3/index.json"
        }
    });
});

Task("Build")
    .IsDependentOn("Restore-NuGet-Packages")
    .Does(() =>
{
    MSBuild(parameters.Paths.Files.Solution, new MSBuildSettings()
        .SetConfiguration(parameters.Configuration)
        //.WithProperty("TreatWarningsAsErrors", "True")
        .UseToolVersion(MSBuildToolVersion.VS2015)
        .SetVerbosity(Verbosity.Minimal)
        .SetNodeReuse(false));
});

Task("Copy-Files")
    .IsDependentOn("Build")
    .Does(() =>
{
    CopyFiles(
        parameters.Paths.Files.ArtifactsSourcePaths,
        parameters.Paths.Directories.ArtifactsBin
    );
});

Task("Create-NuGet-Package")
    .IsDependentOn("Copy-Files")
    .Does(() =>
{
    NuGetPack(parameters.Paths.Files.Nuspec, new NuGetPackSettings {
        Version = parameters.Version.SemVersion,
        //ReleaseNotes = parameters.ReleaseNotes.Notes.ToArray(),
        BasePath = parameters.Paths.Directories.ArtifactsBin,
        OutputDirectory = parameters.Paths.Directories.NugetRoot,
        Symbols = false,
        NoPackageAnalysis = true
    });
});

Task("Run-Integration-Tests")
    .IsDependentOn("Create-NuGet-Package")
    .Does(() =>
{
    var toolsDir = Directory("./example/tools");

    CleanDirectories(toolsDir);

    NuGetInstall("Cake.ServiceFabric", new NuGetInstallSettings {
        Source = new List<string> {
            parameters.Paths.Directories.NugetRoot.MakeAbsolute(Context.Environment).FullPath
        },
        Prerelease = true,
        OutputDirectory = toolsDir,
        ExcludeVersion = true
    });
});

///////////////////////////////////////////////////////////////////////////////
// TASK TARGETS
///////////////////////////////////////////////////////////////////////////////

Task("Default")
  .IsDependentOn("Build");

RunTarget(parameters.Target);