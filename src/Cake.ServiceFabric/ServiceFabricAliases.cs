using System;
using Cake.Core;
using Cake.Core.Annotations;
using Cake.Core.IO;
using Cake.ServiceFabric;
using Cake.ServiceFabric.Utilities;

namespace Cake.ServiceFabric
{
    [CakeAliasCategory("Service Fabric")]
    public static class ServiceFabricAliases
    {
        [CakeMethodAlias]
        [CakeAliasCategory("Connect Cluster")]
        public static IServiceFabricClusterConnection ServiceFabricConnectCluster(
            this ICakeContext context)
        {
            return ServiceFabricConnectCluster(context, new ServiceFabricClusterConnectionSettings());
        }

        [CakeMethodAlias]
        [CakeAliasCategory("Connect Cluster")]
        public static IServiceFabricClusterConnection ServiceFabricConnectCluster(
            this ICakeContext context,
            ServiceFabricClusterConnectionSettings settings)
        {
            return ServiceFabricClusterConnectionFactory.Create(
                context.Registry,
                new PowerShell(context.Log),
                settings);
        }

        [CakeMethodAlias]
        [CakeAliasCategory("Connect Cluster")]
        public static IServiceFabricClusterConnection ServiceFabricConnectCluster(
            this ICakeContext context,
            FilePath publishProfile)
        {
            // TODO: Implement publishProfile to ClusterConnectionSettings
            throw new NotImplementedException();
        }
    }
}
