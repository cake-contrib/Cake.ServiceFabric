using System;
using System.Collections;
using System.Collections.Generic;
using Cake.Core.IO;

namespace Cake.ServiceFabric
{
    public interface IServiceFabricClusterConnection : IDisposable
    {
        void RemoveApplication(Uri applicationName, string applicationTypeName, string applicationTypeVersion);
        void PublishApplication(DirectoryPath applicationPackagePath, FilePath applicationParameterFile);
    }
}