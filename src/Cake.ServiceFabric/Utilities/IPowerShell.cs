using System;
using System.Collections.Generic;
using System.Management.Automation;

namespace Cake.ServiceFabric.Utilities
{
    internal interface IPowerShell : IDisposable
    {
        ICollection<PSObject> Invoke(string command, params string[] arguments);
        ICollection<PSObject> Invoke(string command, Dictionary<string, object> parameters);
    }
}
