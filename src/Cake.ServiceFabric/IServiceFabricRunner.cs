using Cake.Core.IO;

namespace Cake.ServiceFabric
{
    public interface IServiceFabricRunner
    {
        IServiceFabricClusterConnection ConnectCluster();
        IServiceFabricClusterConnection ConnectCluster(FilePath publishProfile);
        IServiceFabricClusterConnection ConnectCluster(ServiceFabricClusterConnectionSettings settings);
    }
}
