using Cake.Core.IO;

namespace Cake.ServiceFabric
{
    public interface IServiceFabricRunner
    {
        void CreatePackage(
            DirectoryPath packagePath,
            FilePath outputFile,
            bool force = false);

        IServiceFabricClusterConnection ConnectCluster();
        IServiceFabricClusterConnection ConnectCluster(FilePath publishProfile);
        IServiceFabricClusterConnection ConnectCluster(ServiceFabricClusterConnectionSettings settings);

        void GetApplicationStatus(IServiceFabricClusterConnection connection, string applicationName);
    }
}
