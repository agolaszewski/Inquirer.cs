using InquirerCS.Interfaces;

namespace InquirerCS.Components
{
    internal class ConfirmPasswordComponent : IConfirmComponent<string>
    {
        private IConsole _console;
        private IWaitForInputComponent<StringOrKey> _input;

        public ConfirmPasswordComponent(IConsole console, IWaitForInputComponent<StringOrKey> input)
        {
            _console = console;
            _input = input;
        }

        public bool Confirm(string result)
        {
            _console.Clear();
            _console.Write("Type again : ");

            StringOrKey repeated = _input.WaitForInput();
            if (repeated.Value != result)
            {
                _console.WriteError("Strings don't match");
                return true;
            }

            return false;
        }
    }
}
