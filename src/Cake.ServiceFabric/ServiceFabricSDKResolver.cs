using System;
using Cake.Core;
using Cake.Core.IO;

namespace Cake.ServiceFabric
{
    internal static class ServiceFabricSDKResolver
    {
        public static FilePath ResolvePSModulePath(IRegistry registry)
        {
            if (registry == null)
            {
                throw new ArgumentNullException(nameof(registry));
            }

            var key = registry.LocalMachine.OpenKey(@"SOFTWARE\Microsoft\Service Fabric SDK");

            if(key == null)
            {
                throw new CakeException("Service Fabric SDK not found.");
            }

            var moduleFolderPath = new DirectoryPath(key.GetValue("FabricSDKPSModulePath").ToString());

            return moduleFolderPath.CombineWithFilePath("ServiceFabricSDK.psm1");
        }
    }
}
