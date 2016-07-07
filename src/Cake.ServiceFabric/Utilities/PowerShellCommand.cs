using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Management.Automation;
using System.Text;
using System.Threading.Tasks;

namespace Cake.ServiceFabric.Utilities
{
    internal sealed class PowerShellCommand : IPowerShellCommand
    {
        private readonly PowerShell _powerShell;

        public PowerShellCommand(PowerShell powerShell)
        {
            if(powerShell == null)
            {
                throw new ArgumentNullException(nameof(powerShell));
            }

            _powerShell = powerShell;
        }

        public void AddArgument(object value)
        {
            _powerShell.AddArgument(value);
        }

        public void AddParameter(string name, object value)
        {
            _powerShell.AddParameter(name, value);
        }

        public void AddParameters(IDictionary parameters)
        {
            _powerShell.AddParameters(parameters);
        }

        public void AddParameters(IList parameters)
        {
            _powerShell.AddParameters(parameters);
        }

        public Collection<PSObject> Invoke()
        {
            return _powerShell.Invoke();
        }

        public Collection<T> Invoke<T>()
        {
            return _powerShell.Invoke<T>();
        }

        public void Dispose()
        {
            _powerShell.Dispose();
        }
    }
}
