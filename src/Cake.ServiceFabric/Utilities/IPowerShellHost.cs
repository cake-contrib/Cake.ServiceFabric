using System;
using System.Collections.Generic;
using System.Management.Automation;

namespace Cake.ServiceFabric.Utilities
{
    internal interface IPowerShellHost : IDisposable
    {
        IPowerShellCommand CreateCommand(string command);
    }
}
