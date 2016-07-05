#load "./paths.cake"
#load "./version.cake"

public class BuildParameters
{
    public string Target { get; private set; }
    public string Configuration { get; private set; }
    public BuildPaths Paths { get; private set; }
    public BuildVersion Version { get; private set; }

    public void SetBuildPaths(BuildPaths paths)
    {
        Paths  = paths;
    }
    
    public void SetBuildVersion(BuildVersion version)
    {
        Version = version;
    }

    public static BuildParameters GetParameters(
        ICakeContext context
        )
    {
        if (context == null)
        {
            throw new ArgumentNullException("context");
        }

        return new BuildParameters {
            Target = context.Argument("target", "Default"),
            Configuration = context.Argument("configuration", "Release")
        };
    }
}
