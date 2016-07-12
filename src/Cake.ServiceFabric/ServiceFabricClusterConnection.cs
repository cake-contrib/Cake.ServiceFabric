using System;
using Cake.ServiceFabric.Utilities;

namespace Cake.ServiceFabric
{
    internal sealed class ServiceFabricClusterConnection : IServiceFabricClusterConnection
    {
        private IPowerShellHost _powerShellHost;

        public ServiceFabricClusterConnection(IPowerShellHost powershellHost)
        {
            if(powershellHost == null)
            {
                throw new ArgumentNullException(nameof(powershellHost));
            }

            _powerShellHost = powershellHost;
        }

        public IServiceFabricAzureActiveDirectoryMetadata AzureActiveDirectoryMetadata { get; set; }

        public string[] ConnectionEndpoint { get; set; }

        public IServiceFabricClientSettings FabricClientSettings { get; set; }

        public IServiceFabricGatewayInformation GatewayInformation { get; set; }

        public void Dispose()
        {
            _powerShellHost.Dispose();
        }

        public void GetApplicationStatus(string applicationName)
        {
            if(string.IsNullOrWhiteSpace(applicationName))
            {
                throw new ArgumentNullException(nameof(applicationName));
            }

            using (var command = _powerShellHost.CreateCommand("Get-ServiceFabricApplicationStatus"))
            {
                command.AddParameter("ApplicationName", applicationName);
                command.Invoke();
            }
        }
    }
}
