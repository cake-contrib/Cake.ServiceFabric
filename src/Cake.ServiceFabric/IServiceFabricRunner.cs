using Cake.Core.IO;

namespace Cake.ServiceFabric
{
    public interface IServiceFabricRunner
    {
        void CreatePackage(
            DirectoryPath packagePath,
            FilePath outputFile,
            bool force = false);
    }
}
