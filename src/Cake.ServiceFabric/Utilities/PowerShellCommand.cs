using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Management.Automation;
using System.Management.Automation.Runspaces;
using System.Text;
using System.Threading.Tasks;
using Cake.Core.Diagnostics;

namespace Cake.ServiceFabric.Utilities
{
    internal sealed class PowerShellCommand : IPowerShellCommand
    {
        private readonly Pipeline _pipeline;
        private readonly ICakeLog _log;

        public PowerShellCommand(Pipeline pipeline, ICakeLog log)
        {
            if(pipeline == null)
            {
                throw new ArgumentNullException(nameof(pipeline));
            }
            if(log == null)
            {
                throw new ArgumentNullException(nameof(log));
            }

            _pipeline = pipeline;
            _log = log;
        }

        public void AddParameter(string name, object value)
        {
            _pipeline.Commands.Last().Parameters.Add(name, value);
        }

        public void AddParameters(Dictionary<string, object> parameters)
        {
            var command = _pipeline.Commands.Last();

            foreach(var parameter in parameters)
            {
                command.Parameters.Add(parameter.Key, parameter.Value);
            }
        }

        public Collection<PSObject> Invoke()
        {
            var command = _pipeline.Commands.Last();
            _log.Debug("Executing command {0} with arguments {1}",
                command.CommandText,
                string.Join(" ", command.Parameters.Select(x => $"-{x.Name} {x.Value}")));
            return _pipeline.Invoke();
        }

        public void Dispose()
        {
            _pipeline.Dispose();
        }
    }
}
