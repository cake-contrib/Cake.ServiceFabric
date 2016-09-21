//#addin "Cake.ServiceFabric"
#addin nuget:?package=Microsoft.ServiceFabric
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
});

Task("Deploy")
    //.IsDependentOn("Build")
    .Does(() =>
{
    #break
    using(var connection = ServiceFabric.ConnectCluster())
    {
        foreach(var app in connection.GetApplications())
        {
            var a = connection.GetApplication(app.ApplicationName);

            Information(string.Format("{0} {1}", a.ApplicationTypeName, a.ApplicationTypeVersion));

            foreach(var service in connection.GetServices(app.ApplicationName))
            {
                var s = connection.GetService(app.ApplicationName, service.ServiceName);

                Information(string.Format("{0} {1}", s.ServiceTypeName, s.ServiceManifestVersion));
            }
        }
    }
});

Task("Default")
    .IsDependentOn("Deploy");

RunTarget(target);