using Cake.Core;
using Cake.Core.Annotations;
using Cake.ServiceFabric.Internal.Fabric;

namespace Cake.ServiceFabric
{
    [CakeAliasCategory("Service Fabric")]
    public static class ServiceFabricAliases
    {
        [CakePropertyAlias]
        public static IServiceFabricRunner ServiceFabric(this ICakeContext context) {
            FabricClientFactory clientFactory = new FabricClientFactory();
            return new ServiceFabricRunner(context.Environment, context.FileSystem, context.Log, clientFactory);
        }
    }
}
