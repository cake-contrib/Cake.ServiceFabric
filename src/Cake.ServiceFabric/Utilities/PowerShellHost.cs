using System;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;
using System.Management.Automation.Runspaces;
using System.Text;
using System.Threading.Tasks;
using Cake.Core.Diagnostics;

namespace Cake.ServiceFabric.Utilities
{
    internal sealed class PowerShellHost : IPowerShellHost
    {
        private Runspace _runspace;
        private ICakeLog _log;

        public PowerShellHost(ICakeLog log)
        {
            if(log == null)
            {
                throw new ArgumentNullException(nameof(log));
            }

            _log = log;
            _runspace = RunspaceFactory.CreateRunspace();
        }

        public IPowerShellCommand CreateCommand(string command)
        {
            var powerShell = _runspace.CreateDisconnectedPowerShell();
            powerShell.Connect();

            return new PowerShellCommand(powerShell);
        }

        public void Dispose()
        {
            _runspace.Dispose();
        }
    }
}
