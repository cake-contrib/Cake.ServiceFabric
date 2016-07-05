public class BuildVersion
{
    public string Version { get; private set; }
    public string SemVersion { get; private set; }
    public string Milestone { get; private set; }

    public static BuildVersion CalculatingSemanticVersion(
        ICakeContext context,
        BuildParameters parameters
        )
    {
        if (context == null)
        {
            throw new ArgumentNullException("context");
        }

        string version = null;
        string semVersion = null;
        string milestone = null;

        context.Information("Calculating Semantic Version");

        context.GitVersion(new GitVersionSettings{
            UpdateAssemblyInfoFilePath = "./src/SolutionInfo.cs",
            UpdateAssemblyInfo = true,
            OutputType = GitVersionOutput.BuildServer
        });

        version = context.EnvironmentVariable("GitVersion_MajorMinorPatch");
        semVersion = context.EnvironmentVariable("GitVersion_LegacySemVerPadded");
        milestone = string.Concat("v", version);

        GitVersion assertedVersions = context.GitVersion(new GitVersionSettings
        {
            OutputType = GitVersionOutput.Json,
        });

        version = assertedVersions.MajorMinorPatch;
        semVersion = assertedVersions.LegacySemVerPadded;
        milestone = string.Concat("v", version);

        context.Information("Calculated Semantic Version: {0}", semVersion);

        return new BuildVersion
        {
            Version = version,
            SemVersion = semVersion,
            Milestone = milestone
        };
    }
}