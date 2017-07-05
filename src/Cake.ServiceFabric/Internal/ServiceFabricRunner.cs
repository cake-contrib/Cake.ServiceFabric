using System;
using Cake.Core;
using Cake.Core.Diagnostics;
using Cake.Core.IO;
using Cake.ServiceFabric.Internal;
using Cake.ServiceFabric.Internal.Fabric;
using static Cake.ServiceFabric.Internal.Utilities.Guard;

namespace Cake.ServiceFabric
{
    internal sealed class ServiceFabricRunner : IServiceFabricRunner
    {
        private readonly ICakeEnvironment _environment;
        private readonly IFileSystem _fileSystem;
        private readonly ICakeLog _log;
        private readonly IFabricClientFactory _clientFactory;

        public ServiceFabricRunner(ICakeEnvironment environment, IFileSystem fileSystem, ICakeLog log, IFabricClientFactory clientFactory)
        {
            _environment = NotNull(environment, nameof(environment));
            _fileSystem = NotNull(fileSystem, nameof(fileSystem));
            _log = NotNull(log, nameof(log));
            _clientFactory = NotNull(clientFactory, nameof(clientFactory));
        }

        public IServiceFabricClusterConnection Connect()
        {
            return Connect(new ServiceFabricClusterConnectionParameters());
        }

        public IServiceFabricClusterConnection Connect(FilePath publishProfile)
        {
            NotNull(publishProfile, nameof(publishProfile));

            var publishProfileParser = new PublishProfileParser(_fileSystem, _log);

            return Connect(publishProfileParser.Parse(publishProfile).ClusterConnectionParameters);
        }

        public IServiceFabricClusterConnection Connect(ServiceFabricClusterConnectionParameters connectionParameters) {
            return new ServiceFabricClusterConnection(_fileSystem, _log, _clientFactory.CreateFabricClient(connectionParameters));
        }
    }
}
