using System;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Xml.Linq;
using Cake.Core;
using Cake.Core.Diagnostics;
using Cake.Core.IO;
using static Cake.ServiceFabric.Internal.Utilities.Guard;

namespace Cake.ServiceFabric.Internal
{
    internal sealed class PublishProfileParser
    {
        private static readonly XNamespace XmlNamespace = @"http://schemas.microsoft.com/2015/05/fabrictools";
        private static readonly XName XClusterConnectionParameters = XmlNamespace + "ClusterConnectionParameters";


        private readonly IFileSystem _fileSystem;
        private readonly ICakeLog _log;

        public PublishProfileParser(IFileSystem fileSystem, ICakeLog log)
        {
            _fileSystem = NotNull(fileSystem, nameof(fileSystem));
            _log = NotNull(log, nameof(log));
        }

        public PublishProfile Parse(FilePath publishProfile)
        {
            NotNull(publishProfile, nameof(publishProfile));

            var file = _fileSystem.GetFile(publishProfile);

            if(!file.Exists)
            {
                throw new CakeException($"PublishProfile '{publishProfile.FullPath}' does not exist.");
            }

            XDocument document;
            using (var stream = file.OpenRead())
            {
                document = XDocument.Load(stream);
            }

            var parameters = document.Root.Element(XClusterConnectionParameters);

            return new PublishProfile
            {
                ClusterConnectionParameters = new ServiceFabricClusterConnectionParameters
                {
                    AzureActiveDirectory = FromAttribute<bool>(parameters, "AzureActiveDirectory"),
                    ConnectionEndpoint = FromAttribute<string>(parameters, "ConnectionEndpoint"),
                    FindType = FromAttribute<X509FindType>(parameters, "FindType"),
                    FindValue = FromAttribute<string>(parameters, "FindValue"),
                    ServerCertThumbprint = FromAttribute<string>(parameters, "ServerCertThumbprint"),
                    StoreLocation = FromAttribute<StoreLocation>(parameters, "StoreLocation"),
                    StoreName = FromAttribute<string>(parameters, "StoreName"),
                    X509Credential = FromAttribute<bool>(parameters, "X509Credential"),
                }
            };
        }

        private T FromAttribute<T>(XElement element, XName attribute)
        {
            return element.Attribute(attribute) != null ?
                (T)Convert.ChangeType(element.Attribute(attribute).Value, typeof(T)) : default(T);
        }
    }
}
