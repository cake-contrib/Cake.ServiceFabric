using System;
using System.Collections.Generic;
using System.Fabric;
using System.Fabric.Description;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Xml;
using Cake.Core.IO;
using static Cake.ServiceFabric.Internal.Utilities.Guard;

namespace Cake.ServiceFabric.Internal.Fabric
{
    internal sealed class FabricClient : IFabricClient
    {
        private readonly System.Fabric.FabricClient _client;

        public FabricClient(System.Fabric.FabricClient _client)
        {
            _client = NotNull(_client, nameof(_client));
        }

        public void Dispose()
        {
            _client.Dispose();
        }

        public async Task RemoveApplicationAsync(Uri applicationName, string applicationTypeName, string applicationTypeVersion)
        {
            await _client.ApplicationManager.DeleteApplicationAsync(applicationName);

            foreach (var node in await _client.QueryManager.GetNodeListAsync())
            {
                foreach (var replica in await _client.QueryManager.GetDeployedReplicaListAsync(node.NodeName, applicationName))
                {
                    var partitionSelector = PartitionSelector.PartitionIdOf(applicationName, replica.Partitionid);
                    var replicaSelector = ReplicaSelector.ReplicaIdOf(partitionSelector, 0L);
                    await _client.FaultManager.RemoveReplicaAsync(replicaSelector, CompletionMode.Verify, true);
                }
            }

            await _client.ApplicationManager.UnprovisionApplicationAsync(applicationTypeName, applicationTypeVersion);
        }

        public async Task PublishApplicationAsync(
            string applicationPackagePath,
            Uri applicationName,
            Dictionary<string, string> applicationParameters,
            string applicationTypeName,
            string applicationTypeVersion
            )
        {
            if (!Directory.Exists(applicationPackagePath))
            {
                throw new ArgumentException("Path does not exist", nameof(applicationPackagePath));
            }

            var app = await _client.QueryManager.GetApplicationListAsync(applicationName);
            if (app.Any())
            {
                // Check also typename and version
                throw new ArgumentException("Application already exists");
            }

            var appType = await _client.QueryManager.GetApplicationTypeListAsync(applicationTypeName);
            if (appType.Any(a => a.ApplicationTypeVersion == applicationTypeVersion))
            {
                throw new ArgumentException("ApplicationType already exists");
            }

            var imageStoreConnectionString = await GetImageStoreConnectionStringAsync();

            _client.ApplicationManager.CopyApplicationPackage(imageStoreConnectionString, applicationPackagePath, applicationTypeName);

            await _client.ApplicationManager.ProvisionApplicationAsync(applicationTypeName);

            _client.ApplicationManager.RemoveApplicationPackage(imageStoreConnectionString, applicationTypeName);

            var applicationDescription = new ApplicationDescription
            {
                ApplicationName = applicationName,
                ApplicationTypeName = applicationTypeName,
                ApplicationTypeVersion = applicationTypeVersion
            };

            foreach (var param in applicationParameters)
            {
                applicationDescription.ApplicationParameters.Add(param.Key, param.Value);
            }

            await _client.ApplicationManager.CreateApplicationAsync(applicationDescription);
        }

        private async Task<string> GetImageStoreConnectionStringAsync()
        {
            var manifest = await _client.ClusterManager.GetClusterManifestAsync();

            var document = new XmlDocument();
            document.LoadXml(manifest);

            var namespaces = new XmlNamespaceManager(document.NameTable);
            namespaces.AddNamespace("sf", "http://schemas.microsoft.com/2011/01/fabric");

            return document.SelectSingleNode(@"//sf:Section[@Name='Management']/sf:Parameter[@Name='ImageStoreConnectionString']/@Value", namespaces).Value;
        }
    }
}
