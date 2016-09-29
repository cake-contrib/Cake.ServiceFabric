using Cake.Core.IO;

namespace Cake.ServiceFabric
{
    public interface IServiceFabricRunner
    {
        IServiceFabricClusterConnection Connect();
        IServiceFabricClusterConnection Connect(ServiceFabricClusterConnectionParameters connectionParameters);
        IServiceFabricClusterConnection Connect(FilePath publishProfile);
    }
}
