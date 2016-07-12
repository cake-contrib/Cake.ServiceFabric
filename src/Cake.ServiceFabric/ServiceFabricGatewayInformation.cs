using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cake.ServiceFabric
{
    internal class ServiceFabricGatewayInformation : IServiceFabricGatewayInformation
    {
        public string NodeAddress { get; set; }
        public string NodeId { get; set; }
        public string NodeInstanceId { get; set; }
        public string NodeName { get; set; }
    }
}
