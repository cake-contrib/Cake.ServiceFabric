using System;
using System.Collections;
using System.Collections.ObjectModel;
using System.Management.Automation;

namespace Cake.ServiceFabric.Utilities
{
    public interface IPowerShellCommand : IDisposable
    {
        void AddArgument(object value);
        void AddParameter(string name, object value);
        void AddParameters(IDictionary parameters);
        void AddParameters(IList parameters);
        Collection<PSObject> Invoke();
        Collection<T> Invoke<T>();
    }
}