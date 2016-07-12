using System;

namespace Cake.ServiceFabric
{
    public interface IServiceFabricGatewayInformation
    {
        string NodeAddress { get; }
        string NodeId { get; }
        string NodeInstanceId { get; }
        string NodeName { get; }
    }
}