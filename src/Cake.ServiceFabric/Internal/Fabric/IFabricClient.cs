using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Cake.ServiceFabric.Internal.Fabric
{
    internal interface IFabricClient : IDisposable
    {
        Task RemoveApplicationAsync(Uri applicationName, string applicationTypeName, string applicationTypeVersion);
        Task PublishApplicationAsync(string applicationPackagePath, Uri applicationName, Dictionary<string, string> applicationParameters, string applicationTypeName, string applicationTypeVersion);
    }
}
