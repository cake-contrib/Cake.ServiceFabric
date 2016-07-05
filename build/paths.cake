public class BuildPaths
{
    public BuildFiles Files { get; private set; }
    public BuildDirectories Directories { get; private set; }

    public static BuildPaths GetPaths(
        ICakeContext context,
        string configuration,
        string semVersion
        )
    {
        if (context == null)
        {
            throw new ArgumentNullException("context");
        }

        if (string.IsNullOrEmpty(configuration))
        {
            throw new ArgumentNullException("configuration");
        }

        if (string.IsNullOrEmpty(semVersion))
        {
            throw new ArgumentNullException("semVersion");
        }

        var buildDir = context.Directory("./src/Cake.ServiceFabric/bin") + context.Directory(configuration);
        var artifactsDir = (DirectoryPath)(context.Directory("./artifacts") + context.Directory("v" + semVersion));
        var artifactsBinDir = artifactsDir.Combine("bin");
        var testResultsDir = artifactsDir.Combine("test-results");
        var nugetRoot = artifactsDir.Combine("nuget");

        var assemblyPaths = new FilePath[] {
            buildDir + context.File("Cake.ServiceFabric.dll"),
            buildDir + context.File("Cake.ServiceFabric.pdb"),
            buildDir + context.File("Cake.ServiceFabric.xml"),
            buildDir + context.File("Microsoft.Management.Infrastructure.dll"),
            buildDir + context.File("System.Management.Automation.dll")
        };

        var repoFilesPaths = new FilePath[] {
            "LICENSE",
            "README.md",
            "ReleaseNotes.md"
        };

        var artifactSourcePaths = assemblyPaths.Concat(repoFilesPaths).ToArray();

        var buildDirectories = new BuildDirectories(
            artifactsDir,
            testResultsDir,
            nugetRoot,
            artifactsBinDir
            );

        var buildFiles = new BuildFiles(
            context.File("./src/Cake.ServiceFabric.sln"),
            context.File("./nuspec/Cake.ServiceFabric.nuspec"),
            assemblyPaths,
            repoFilesPaths,
            artifactSourcePaths
            );

        return new BuildPaths
        {
            Files = buildFiles,
            Directories = buildDirectories
        };
    }
}

public class BuildFiles
{
    public FilePath Solution { get; private set; }
    public FilePath Nuspec { get; private set; }
    public ICollection<FilePath> AssemblyPaths { get; private set; }
    public ICollection<FilePath> RepoFilesPaths { get; private set; }
    public ICollection<FilePath> ArtifactsSourcePaths { get; private set; }

    public BuildFiles(
        FilePath solution,
        FilePath nuspec,
        FilePath[] assemblyPaths,
        FilePath[] repoFilesPaths,
        FilePath[] artifactsSourcePaths
        )
    {
        Solution = solution;
        Nuspec = nuspec;
        AssemblyPaths = assemblyPaths;
        RepoFilesPaths = repoFilesPaths;
        ArtifactsSourcePaths = artifactsSourcePaths;
    }
}

public class BuildDirectories
{
    public DirectoryPath Artifacts { get; private set; }
    public DirectoryPath TestResults { get; private set; }
    public DirectoryPath NugetRoot { get; private set; }
    public DirectoryPath ArtifactsBin { get; private set; }
    public ICollection<DirectoryPath> ToClean { get; private set; }

    public BuildDirectories(
        DirectoryPath artifactsDir,
        DirectoryPath testResultsDir,
        DirectoryPath nugetRoot,
        DirectoryPath artifactsBinDir
        )
    {
        Artifacts = artifactsDir;
        TestResults = testResultsDir;
        NugetRoot = nugetRoot;
        ArtifactsBin = artifactsBinDir;
        ToClean = new[] {
            Artifacts,
            TestResults,
            NugetRoot,
            ArtifactsBin
        };
    }
}