using System.Security.Cryptography.X509Certificates;

namespace Cake.ServiceFabric
{
    public class ServiceFabricClusterConnectionParameters
    {
        public string ConnectionEndpoint { get; set; }
        public bool X509Credential { get; set; }
        public string ServerCertThumbprint { get; set; }
        public X509FindType FindType { get; set; }
        public string FindValue { get; set; }
        public StoreLocation StoreLocation { get; set; }
        public string StoreName { get; set; }
        public bool AzureActiveDirectory { get; set; }
    }
}