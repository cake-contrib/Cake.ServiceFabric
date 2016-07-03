using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cake.Core.IO;
using Cake.ServiceFabric.Utilities;

namespace Cake.ServiceFabric
{
    internal static class ServiceFabricClusterConnectionFactory
    {
        public static IServiceFabricClusterConnection Create(IRegistry registry, IPowerShell powershell, ServiceFabricClusterConnectionSettings settings)
        {
            var connection = new ServiceFabricClusterConnection(powershell);
            var sdkModulePath = ServiceFabricSDKResolver.ResolvePSModulePath(registry);
            connection.Connect(settings, sdkModulePath);

            return connection;
        }
    }
}
