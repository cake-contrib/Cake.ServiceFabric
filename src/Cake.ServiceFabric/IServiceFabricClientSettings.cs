using System;

namespace Cake.ServiceFabric
{
    public interface IServiceFabricClientSettings
    {
        string ClientFriendlyName { get; }
        long PartitionLocationCacheLimit { get; }
        long PartitionLocationCacheBucketCount { get; }
        TimeSpan ServiceChangePollInterval { get; }
        TimeSpan ConnectionInitializationTimeout { get; }
        TimeSpan KeepAliveInterval { get; }
        TimeSpan HealthOperationTimeout { get; }
        TimeSpan HealthReportSendInterval { get; }
        TimeSpan HealthReportRetrySendInterval { get; }
        TimeSpan NotificationGatewayConnectionTimeout { get; }
        TimeSpan NotificationCacheUpdateTimeout { get; }
        long AuthTokenBufferSize { get; }
    }
}