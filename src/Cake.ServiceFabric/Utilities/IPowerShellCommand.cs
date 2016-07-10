using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Management.Automation;

namespace Cake.ServiceFabric.Utilities
{
    public interface IPowerShellCommand : IDisposable
    {
        void AddParameter(string name, object value);
        void AddParameters(Dictionary<string, object> parameters);
        Collection<PSObject> Invoke();
    }
}