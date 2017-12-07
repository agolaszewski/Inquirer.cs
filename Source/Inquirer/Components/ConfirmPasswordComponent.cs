using System;
using InquirerCS.Interfaces;

namespace InquirerCS.Components
{
    internal class ConfirmPasswordComponent : IConfirmComponent<string>
    {
        private IWaitForInputComponent<string> _input;

        public ConfirmPasswordComponent(IWaitForInputComponent<string> inputComponent)
        {
            _input = inputComponent;
        }

        public bool Confirm(string result)
        {
            Console.Clear();
            ConsoleHelper.Write("Type again : ");

            var repeated = _input.WaitForInput();
            if (repeated != result)
            {
                ConsoleHelper.WriteError("Strings don't match");
                return true;
            }

            return false;
        }
    }
}
