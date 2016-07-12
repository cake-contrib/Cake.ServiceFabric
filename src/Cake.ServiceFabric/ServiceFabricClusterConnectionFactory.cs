using System;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;
using System.Text;
using System.Threading.Tasks;
using Cake.ServiceFabric.Utilities;

namespace Cake.ServiceFabric
{
    internal static class ServiceFabricClusterConnectionFactory
    {
        internal static IServiceFabricClusterConnection CreateConnection(IPowerShellHost host, PSObject obj)
        {
            string[] connectionEndpoint = obj.Properties["ConnectionEndpoint"].Value as string[];
            dynamic fabricClientSettings = obj.Properties["FabricClientSettings"].Value;
            dynamic gatewayInformation = obj.Properties["GatewayInformation"].Value;
            dynamic azureActiveDirectoryMetadata = obj.Properties["AzureActiveDirectoryMetadata"].Value;

            return new ServiceFabricClusterConnection(host)
            {
                ConnectionEndpoint = connectionEndpoint,
                FabricClientSettings = fabricClientSettings == null ? null : new ServiceFabricClientSettings
                {
                    ClientFriendlyName = fabricClientSettings.ClientFriendlyName,
                    PartitionLocationCacheLimit = fabricClientSettings.PartitionLocationCacheLimit,
                    PartitionLocationCacheBucketCount = fabricClientSettings.PartitionLocationCacheBucketCount,
                    ServiceChangePollInterval = fabricClientSettings.ServiceChangePollInterval,
                    ConnectionInitializationTimeout = fabricClientSettings.ConnectionInitializationTimeout,
                    KeepAliveInterval = fabricClientSettings.KeepAliveInterval,
                    HealthOperationTimeout = fabricClientSettings.HealthOperationTimeout,
                    HealthReportSendInterval = fabricClientSettings.HealthReportSendInterval,
                    HealthReportRetrySendInterval = fabricClientSettings.HealthReportRetrySendInterval,
                    NotificationGatewayConnectionTimeout = fabricClientSettings.NotificationGatewayConnectionTimeout,
                    NotificationCacheUpdateTimeout = fabricClientSettings.NotificationCacheUpdateTimeout,
                    AuthTokenBufferSize = fabricClientSettings.AuthTokenBufferSize
                },
                GatewayInformation = gatewayInformation == null ? null : new ServiceFabricGatewayInformation
                {
                    NodeAddress = gatewayInformation.NodeAddress,
                    NodeId = gatewayInformation.NodeId.ToString(),
                    NodeInstanceId = gatewayInformation.NodeInstanceId.ToString(),
                    NodeName = gatewayInformation.NodeName
                },
                AzureActiveDirectoryMetadata = azureActiveDirectoryMetadata == null ? null : new ServiceFabricAzureActiveDirectoryMetadata
                {

                }
            };
        }
    }
}
