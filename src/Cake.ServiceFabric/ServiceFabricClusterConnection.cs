using System;
using System.Collections.Generic;
using System.Fabric.Query;
using System.Linq;
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

        public IEnumerable<Application> GetApplications()
        {
            using (var command = _powerShellHost.CreateCommand("Get-ServiceFabricApplication"))
            {
                var result = command.Invoke();

                return result
                    .Where(x => x.TypeNames.Contains("System.Fabric.Query.Application"))
                    .Select(x => x.BaseObject as Application);
            }
        }

        public Application GetApplication(Uri applicationName)
        {
            using (var command = _powerShellHost.CreateCommand("Get-ServiceFabricApplication"))
            {
                command.AddParameter("ApplicationName", applicationName);
                var result = command.Invoke();

                return result
                    .First(x => x.TypeNames.Contains("System.Fabric.Query.Application"))
                    .BaseObject as Application;
            }
        }

        public IEnumerable<Service> GetServices(Uri applicationName)
        {
            using (var command = _powerShellHost.CreateCommand("Get-ServiceFabricService"))
            {
                command.AddParameter("ApplicationName", applicationName);
                var result = command.Invoke();

                return result
                    .Where(x => x.TypeNames.Contains("System.Fabric.Query.Service"))
                    .Select(x => x.BaseObject as Service);
            }
        }

        public Service GetService(Uri applicationName, Uri serviceName)
        {
            using (var command = _powerShellHost.CreateCommand("Get-ServiceFabricService"))
            {
                command.AddParameter("ApplicationName", applicationName);
                command.AddParameter("ServiceName", serviceName);

                var result = command.Invoke();

                return result
                    .First(x => x.TypeNames.Contains("System.Fabric.Query.Service"))
                    .BaseObject as Service;
            }
        }
    }
}
