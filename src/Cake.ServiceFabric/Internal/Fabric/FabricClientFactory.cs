using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cake.ServiceFabric.Internal.Fabric
{
    internal class FabricClientFactory : IFabricClientFactory
    {
        public IFabricClient CreateFabricClient(ServiceFabricClusterConnectionParameters clusterConnectionParameters)
        {
            if(clusterConnectionParameters.X509Credential)
            {
                var credentials = new System.Fabric.X509Credentials();

                // Client certificate
                credentials.StoreLocation = clusterConnectionParameters.StoreLocation;
                credentials.StoreName = clusterConnectionParameters.StoreName;
                credentials.FindType = clusterConnectionParameters.FindType;
                credentials.FindValue = clusterConnectionParameters.FindValue;

                // Server certificate
                credentials.RemoteCertThumbprints.Add(clusterConnectionParameters.ServerCertThumbprint);
                //xc.RemoteCommonNames.Add(name); //TODO

                credentials.ProtectionLevel = System.Fabric.ProtectionLevel.EncryptAndSign;

                return new FabricClient(new System.Fabric.FabricClient(credentials, clusterConnectionParameters.ConnectionEndpoint));
            }
            else if(clusterConnectionParameters.AzureActiveDirectory)
            {
                // TODO
                throw new NotImplementedException();
            }
            else
            {
                return new FabricClient(new System.Fabric.FabricClient());
            }
        }
    }
}
