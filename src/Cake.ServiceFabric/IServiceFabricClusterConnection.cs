using System;
using System.Collections.Generic;
using System.Fabric.Query;

namespace Cake.ServiceFabric
{
    public interface IServiceFabricClusterConnection : IDisposable
    {
        string [] ConnectionEndpoint { get; }
        IServiceFabricClientSettings FabricClientSettings { get; }
        IServiceFabricGatewayInformation GatewayInformation { get; }
        IServiceFabricAzureActiveDirectoryMetadata AzureActiveDirectoryMetadata { get; }

        IEnumerable<Application> GetApplications();
        Application GetApplication(Uri applicationName);

        IEnumerable<Service> GetServices(Uri applicationName);
        Service GetService(Uri applicationName, Uri serviceName);
    }
}