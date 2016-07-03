using System;

namespace Cake.ServiceFabric
{
    public interface IServiceFabricClusterConnection : IDisposable
    {
        ServiceFabricApplicationStatus GetApplicationStatus(string applicationName);
    }
}