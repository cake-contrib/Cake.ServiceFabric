using Cake.Core;
using Cake.Core.Annotations;

namespace Cake.ServiceFabric
{
    [CakeAliasCategory("Service Fabric")]
    public static class ServiceFabricAliases
    {
        [CakePropertyAlias]
        public static IServiceFabricRunner ServiceFabric(this ICakeContext context)
        {
            return new ServiceFabricRunner(context);
        }
    }
}
