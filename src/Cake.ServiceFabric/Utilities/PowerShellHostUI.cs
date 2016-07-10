using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Management.Automation;
using System.Management.Automation.Host;
using System.Security;
using Cake.Core.Diagnostics;

namespace Cake.ServiceFabric.Utilities
{
    internal class PowerShellHostUI : PSHostUserInterface
    {
        private readonly ICakeLog _log;
        private readonly PSHostRawUserInterface _rawUI;

        public PowerShellHostUI(ICakeLog log)
        {
            if(log == null)
            {
                throw new ArgumentNullException(nameof(log));
            }

            _log = log;
            _rawUI = new PowerShellHostRawUI(log);
        }

        public override PSHostRawUserInterface RawUI => _rawUI;

        public override Dictionary<string, PSObject> Prompt(string caption, string message, Collection<FieldDescription> descriptions)
        {
            throw new NotImplementedException();
        }

        public override int PromptForChoice(string caption, string message, Collection<ChoiceDescription> choices, int defaultChoice)
        {
            throw new NotImplementedException();
        }

        public override PSCredential PromptForCredential(string caption, string message, string userName, string targetName)
        {
            throw new NotImplementedException();
        }

        public override PSCredential PromptForCredential(string caption, string message, string userName, string targetName, PSCredentialTypes allowedCredentialTypes, PSCredentialUIOptions options)
        {
            throw new NotImplementedException();
        }

        public override string ReadLine()
        {
            throw new NotImplementedException();
        }

        public override SecureString ReadLineAsSecureString()
        {
            throw new NotImplementedException();
        }

        public override void Write(string value)
        {
            _log.Write(Verbosity.Normal, LogLevel.Information, value);
        }

        public override void Write(ConsoleColor foregroundColor, ConsoleColor backgroundColor, string value)
        {
            _log.Write(Verbosity.Normal, LogLevel.Information, value);
        }

        public override void WriteDebugLine(string message)
        {
            _log.Write(Verbosity.Diagnostic, LogLevel.Debug, message); ;
        }

        public override void WriteErrorLine(string value)
        {
            _log.Write(Verbosity.Normal, LogLevel.Error, value);
        }

        public override void WriteLine(string value)
        {
            Write(value);
        }

        public override void WriteProgress(long sourceId, ProgressRecord record)
        {
            return;
        }

        public override void WriteVerboseLine(string value)
        {
            _log.Write(Verbosity.Verbose, LogLevel.Verbose, value);
        }

        public override void WriteWarningLine(string value)
        {
            _log.Write(Verbosity.Normal, LogLevel.Warning, value);
        }
    }
}