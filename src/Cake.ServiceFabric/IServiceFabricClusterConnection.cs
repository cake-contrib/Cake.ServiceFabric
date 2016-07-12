using System;

namespace Cake.ServiceFabric
{
    public interface IServiceFabricClusterConnection : IDisposable
    {
        string [] ConnectionEndpoint { get; }
        IServiceFabricClientSettings FabricClientSettings { get; }
        IServiceFabricGatewayInformation GatewayInformation { get; }
        IServiceFabricAzureActiveDirectoryMetadata AzureActiveDirectoryMetadata { get; }

        void GetApplicationStatus(string applicationName);
    }
}