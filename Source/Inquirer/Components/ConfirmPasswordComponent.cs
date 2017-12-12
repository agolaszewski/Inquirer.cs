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
            AppConsole2.Write("Type again : ");

            var repeated = _input.WaitForInput();
            if (repeated != result)
            {
                AppConsole2.WriteError("Strings don't match");
                return true;
            }

            return false;
        }
    }
}
