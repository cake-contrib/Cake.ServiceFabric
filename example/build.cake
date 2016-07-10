#addin "Cake.ServiceFabric"

var target = Argument("target", "Default");

Task("Build")
    .Does(() =>
{
    MSBuild("./ExampleApp/ExampleApp.sfproj", 
      new MSBuildSettings {
        Configuration = "Release",
        PlatformTarget = PlatformTarget.x64
      }.WithTarget("Package"));

    ServiceFabric.CreatePackage(
      Directory("./ExampleApp/pkg/Release"),
      File("./tools/package.sfproj")
    );
});

Task("Deploy")
    .IsDependentOn("Build")
    .Does(() =>
{
    using(var connection = ServiceFabric.ConnectCluster())
    {
        ServiceFabric.GetApplicationStatus(connection, "fabric:/ExampleApp");
    }
});

Task("Default")
    .IsDependentOn("Deploy");

RunTarget(target);