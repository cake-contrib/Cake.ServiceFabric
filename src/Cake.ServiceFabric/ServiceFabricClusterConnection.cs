using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Management.Automation;
using Cake.ServiceFabric.Utilities;
using System.Collections;
using Cake.Core.IO;

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

        public void Connect(ServiceFabricClusterConnectionSettings settings, FilePath sdkModulePath)
        {
            if (settings == null)
            {
                throw new ArgumentNullException(nameof(settings));
            }

            using (var command = _powerShellHost.CreateCommand("Import-Module"))
            {
                command.AddParameter("name", sdkModulePath.FullPath.Replace("/", "\\"));
                command.Invoke();
            }
        }

        public void Dispose()
        {
            _powerShellHost.Dispose();
        }

        public ServiceFabricApplicationStatus GetApplicationStatus(string applicationName)
        {
            if(string.IsNullOrWhiteSpace(applicationName))
            {
                throw new ArgumentNullException(nameof(applicationName));
            }

            //var output = _powerShellHost.Invoke(
            //    "Get-ServiceFabricApplicationStatus", 
            //    new Dictionary<string, object> {
            //        { "ApplicationName",  applicationName }
            //    });

            return new ServiceFabricApplicationStatus();
        }
    }
}
