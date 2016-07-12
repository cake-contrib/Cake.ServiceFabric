//#addin "Cake.ServiceFabric"
#r "../src/Cake.ServiceFabric/bin/Debug/Cake.ServiceFabric.dll"

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
      File("./tools/package.sfproj"),
      true);
});

Task("Deploy")
    //.IsDependentOn("Build")
    .Does(() =>
{
    #break
    using(var connection = ServiceFabric.ConnectCluster())
    {
        connection.GetApplicationStatus("fabric:/ExampleApp");
    }
});

Task("Default")
    .IsDependentOn("Deploy");

RunTarget(target);