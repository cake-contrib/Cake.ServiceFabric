using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cake.ServiceFabric
{
    class ServiceFabricClientSettings : IServiceFabricClientSettings
    {
        public string ClientFriendlyName { get; set; }
        public long PartitionLocationCacheLimit { get; set; }
        public long PartitionLocationCacheBucketCount { get; set; }
        public TimeSpan ServiceChangePollInterval { get; set; }
        public TimeSpan ConnectionInitializationTimeout { get; set; }
        public TimeSpan KeepAliveInterval { get; set; }
        public TimeSpan HealthOperationTimeout { get; set; }
        public TimeSpan HealthReportSendInterval { get; set; }
        public TimeSpan HealthReportRetrySendInterval { get; set; }
        public TimeSpan NotificationGatewayConnectionTimeout { get; set; }
        public TimeSpan NotificationCacheUpdateTimeout { get; set; }
        public long AuthTokenBufferSize { get; set; }
    }
}
