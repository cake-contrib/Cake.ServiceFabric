using System;
using System.Collections.Generic;
using System.Linq;
using Cake.Core;
using Cake.Core.IO;
using Cake.ServiceFabric.Extensions;
using Cake.ServiceFabric.Utilities;

namespace Cake.ServiceFabric
{
    internal sealed class ServiceFabricRunner : IServiceFabricRunner
    {
        private readonly ICakeEnvironment _environment;
        private readonly IPowerShellHost _host;

        public ServiceFabricRunner(ICakeEnvironment environment, IPowerShellHost host)
        {
            _environment = environment;
            _host = host;
        }

        public void CreatePackage(DirectoryPath packagePath, FilePath outputFile, bool force = false)
        {
            using (var command = _host.CreateCommand("New-ServiceFabricApplicationPackage"))
            {
                command.AddParameters(new Dictionary<string, object>
                {
                    { "ApplicationPackagePath", packagePath.MakeAbsolute(_environment).FullPathEscaped() },
                    { "SFpkgName", outputFile.GetFilename() },
                    { "SFpkgOutputPath", outputFile.GetDirectory().MakeAbsolute(_environment).FullPathEscaped() },
                    { "Force", force }
                });

                command.Invoke();
            }
        }

        public IServiceFabricClusterConnection ConnectCluster()
        {
            using (var command = _host.CreateCommand("Connect-ServiceFabricCluster"))
            {
                var result = command.Invoke();

                return ServiceFabricClusterConnectionFactory.CreateConnection(_host, 
                    result.First(x => x.TypeNames.Contains("Microsoft.ServiceFabric.Powershell.ClusterConnection")));
            }
        }

        public IServiceFabricClusterConnection ConnectCluster(FilePath publishProfile)
        {
            throw new NotImplementedException();
        }

        public IServiceFabricClusterConnection ConnectCluster(ServiceFabricClusterConnectionSettings settings)
        {
            throw new NotImplementedException();
        }
    }
}
