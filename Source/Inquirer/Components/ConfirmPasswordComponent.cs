using System;
using InquirerCS.Interfaces;

namespace InquirerCS.Components
{
    internal class ConfirmPasswordComponent : IConfirmComponent<string>
    {
        private IConsole _console;
        private IWaitForInputComponent<string> _input;

        public ConfirmPasswordComponent(IWaitForInputComponent<string> inputComponent, IConsole console)
        {
            _input = inputComponent;
            _console = console;
        }

        public bool Confirm(string result)
        {
            _console.Clear();
            _console.Write("Type again : ");

            var repeated = _input.WaitForInput();
            if (repeated != result)
            {
                _console.WriteError("Strings don't match");
                return true;
            }

            return false;
        }
    }
}
