using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cake.ServiceFabric.Internal.Fabric
{
    internal interface IFabricClientFactory
    {
        IFabricClient CreateFabricClient(ServiceFabricClusterConnectionParameters clusterConnectionParameters);
    }
}
