using System;
using InquirerCS.Interfaces;

namespace InquirerCS.Components
{
    internal class ConfirmPasswordComponent : IConfirmComponent<string>
    {
        private IWaitForInputComponent<string> _inputComponent;

        public ConfirmPasswordComponent(IWaitForInputComponent<string> inputComponent)
        {
            _inputComponent = inputComponent;
        }

        public bool Confirm(string result)
        {
            Console.Clear();
            ConsoleHelper.Write("Type again : ");

            string repeated = _inputComponent.WaitForInput();
            if (repeated != result)
            {
                ConsoleHelper.WriteError("Strings doesn't match");
                return true;
            }

            return false;
        }
    }
}
