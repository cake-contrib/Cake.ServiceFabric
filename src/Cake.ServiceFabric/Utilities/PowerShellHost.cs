using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Management.Automation;
using System.Management.Automation.Host;
using System.Management.Automation.Runspaces;
using System.Text;
using System.Threading.Tasks;
using Cake.Core.Diagnostics;
using Cake.Core.IO;
using Cake.ServiceFabric.Extensions;

namespace Cake.ServiceFabric.Utilities
{
    internal sealed class PowerShellHost : PSHost, IPowerShellHost
    {
        private readonly Runspace _runspace;
        private readonly ICakeLog _log;
        private readonly Guid _instanceId;
        private readonly PSHostUserInterface _userInterface;
        private readonly FilePath _sdkModulePath;

        public override CultureInfo CurrentCulture => CultureInfo.CurrentCulture;

        public override CultureInfo CurrentUICulture => CultureInfo.CurrentUICulture;

        public override Guid InstanceId => _instanceId;

        public override string Name => "Cake.ServiceFabric.PowerShellHost";

        public override PSHostUserInterface UI => _userInterface;

        public override Version Version => new Version(0, 1, 0);

        public PowerShellHost(ICakeLog log, IRegistry registry)
        {
            if(log == null)
            {
                throw new ArgumentNullException(nameof(log));
            }

            _log = log;
            _instanceId = Guid.NewGuid();
            _userInterface = new PowerShellHostUI(log);
            _runspace = RunspaceFactory.CreateRunspace(this);
            _sdkModulePath = ServiceFabricSDKResolver.ResolvePSModulePath(registry);

            _runspace.Open();


            var pipeline = _runspace.CreatePipeline();
            pipeline.Commands.AddScript("Import-Module " + _sdkModulePath.FullPathQouted());
            pipeline.Invoke();
        }

        public IPowerShellCommand CreateCommand(string command)
        {
            var pipeline = _runspace.CreatePipeline();

            pipeline.Commands.Add(command);

            return new PowerShellCommand(pipeline, _log);
        }

        public void Dispose()
        {
            _runspace.Dispose();
        }

        public override void SetShouldExit(int exitCode)
        {
            return;
        }

        public override void EnterNestedPrompt()
        {
            return;
        }

        public override void ExitNestedPrompt()
        {
            return;
        }

        public override void NotifyBeginApplication()
        {
            return;
        }

        public override void NotifyEndApplication()
        {
            return;
        }
    }
}
