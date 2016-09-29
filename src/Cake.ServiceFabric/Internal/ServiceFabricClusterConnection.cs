using System;
using Cake.Core;
using Cake.Core.Diagnostics;
using Cake.Core.IO;
using Cake.ServiceFabric.Internal.Fabric;
using static Cake.ServiceFabric.Internal.Utilities.Guard;

namespace Cake.ServiceFabric
{
    internal sealed class ServiceFabricClusterConnection : IServiceFabricClusterConnection
    {
        private readonly ICakeEnvironment _environment;
        private readonly IFileSystem _fileSystem;
        private readonly ICakeLog _log;
        private readonly IFabricClient _client;

        public ServiceFabricClusterConnection(ICakeEnvironment environment, IFileSystem fileSystem, ICakeLog log, IFabricClient client)
        {
            _environment = NotNull(environment, nameof(environment));
            _fileSystem = NotNull(fileSystem, nameof(fileSystem));
            _log = NotNull(log, nameof(log));
            _client = NotNull(client, nameof(client));
        }

        public void Dispose()
        {
            _client.Dispose();
        }

        public void RemoveApplication(Uri applicationName, string applicationTypeName, string applicationTypeVersion)
        {
            _client.RemoveApplicationAsync(applicationName, applicationTypeName, applicationTypeVersion).Wait();
        }

        public void PublishApplication(DirectoryPath applicationPackagePath, FilePath applicationParameterFile)
        {
            throw new NotImplementedException();
        }
    }
}
