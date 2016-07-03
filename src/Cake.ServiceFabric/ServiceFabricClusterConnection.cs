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
        private IPowerShell _powerShell;

        public ServiceFabricClusterConnection(IPowerShell powershell)
        {
            if(powershell == null)
            {
                throw new ArgumentNullException(nameof(powershell));
            }

            _powerShell = powershell;
        }

        public void Connect(ServiceFabricClusterConnectionSettings settings, FilePath sdkModulePath)
        {
            if (settings == null)
            {
                throw new ArgumentNullException(nameof(settings));
            }
            
            _powerShell.Invoke("Import-Module", sdkModulePath.FullPath.Replace("/","\\"));
            var output = _powerShell.Invoke("Connect-ServiceFabricCluster");
        }

        public void Dispose()
        {
            _powerShell.Dispose();
        }

        public ServiceFabricApplicationStatus GetApplicationStatus(string applicationName)
        {
            if(string.IsNullOrWhiteSpace(applicationName))
            {
                throw new ArgumentNullException(nameof(applicationName));
            }

            var output = _powerShell.Invoke(
                "Get-ServiceFabricApplicationStatus", 
                new Dictionary<string, object> {
                    { "ApplicationName",  applicationName }
                });

            return new ServiceFabricApplicationStatus();
        }
    }
}
