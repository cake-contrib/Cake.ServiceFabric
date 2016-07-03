using System;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;
using System.Text;
using System.Threading.Tasks;
using Cake.Core.Diagnostics;

namespace Cake.ServiceFabric.Utilities
{
    internal sealed class PowerShell : IPowerShell
    {
        private System.Management.Automation.PowerShell _powershell;
        private ICakeLog _log;

        public PowerShell(ICakeLog log)
        {
            if(log == null)
            {
                throw new ArgumentNullException(nameof(log));
            }

            _log = log;
            _powershell = System.Management.Automation.PowerShell.Create();
        }

        public void Dispose()
        {
            _powershell.Dispose();
        }

        public ICollection<PSObject> Invoke(string command, Dictionary<string, object> parameters)
        {
            _powershell.Commands.Clear();
            _log.Debug("Executing command {0} with params {1}", command, string.Join(";", parameters.Select(x => x.Key + "=" + x.Value.ToString())));

            _powershell.AddCommand(command);
            _powershell.AddParameters(parameters);

            return _powershell.Invoke();
        }

        public ICollection<PSObject> Invoke(string command, params string[] arguments)
        {
            _powershell.Commands.Clear();
            _log.Debug("Executing command {0} with args {1}", command, string.Join(";", arguments));

            _powershell.AddCommand(command);
            foreach(var argument in arguments)
            {
                _powershell.AddArgument(argument);
            }

            return _powershell.Invoke();
        }
    }
}
