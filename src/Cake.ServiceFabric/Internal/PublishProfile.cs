using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Cake.Core.IO;
using static Cake.ServiceFabric.Internal.Utilities.Guard;

namespace Cake.ServiceFabric.Internal
{
    internal sealed class PublishProfile
    {
        public ServiceFabricClusterConnectionParameters ClusterConnectionParameters { get; set; }
    }
}
