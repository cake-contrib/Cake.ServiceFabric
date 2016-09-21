using Cake.Core;
using Cake.Core.Annotations;
using Cake.ServiceFabric.Utilities;

namespace Cake.ServiceFabric
{
    [CakeAliasCategory("Service Fabric")]
    public static class ServiceFabricAliases
    {
        [CakePropertyAlias]
        public static IServiceFabricRunner ServiceFabric(this ICakeContext context)
        {
            return new ServiceFabricRunner(context.Environment, new PowerShellHost(context.Log));
        }
    }
}
