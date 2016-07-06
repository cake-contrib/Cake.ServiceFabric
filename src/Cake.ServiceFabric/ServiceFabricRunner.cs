using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cake.Core;
using Cake.Core.IO;
using Cake.ServiceFabric.Utilities;

namespace Cake.ServiceFabric
{
    internal sealed class ServiceFabricRunner : IServiceFabricRunner
    {
        private readonly ICakeEnvironment _environment;
        private readonly IPowerShell _powerShell;
        private readonly FilePath _sdkModulePath;

        public ServiceFabricRunner(IRegistry registry, ICakeEnvironment environment, IPowerShell powershell)
        {
            _environment = environment;
            _sdkModulePath = ServiceFabricSDKResolver.ResolvePSModulePath(registry);
            _powerShell = powershell;
        }

        public void CreatePackage(DirectoryPath packagePath, FilePath outputFile, bool force = false)
        {
            _powerShell.Invoke("Import-Module", _sdkModulePath.FullPath.Replace("/", "\\"));
            _powerShell.Invoke("New-ServiceFabricApplicationPackage",
                new Dictionary<string, object>
                {
                    { "ApplicationPackagePath", packagePath.MakeAbsolute(_environment).FullPath.Replace("/", "\\") },
                    { "SFpkgName", outputFile.GetFilename() },
                    { "SFpkgOutputPath", outputFile.GetDirectory().MakeAbsolute(_environment).FullPath.Replace("/", "\\") },
                    { "Force", force }
                });
        }
    }
}
