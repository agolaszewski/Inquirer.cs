using InquirerCS.Interfaces;

namespace InquirerCS.Components
{
    public class ConfirmPasswordComponent : IConfirmComponent<string>
    {
        private IConsole _console;

        public ConfirmPasswordComponent(IConsole console)
        {
            _console = console;
        }

        public bool Confirm(string result)
        {
            _console.Clear();
            _console.Write("Type again : ");

            var repeated = _console.Read();
            if (repeated != result)
            {
                _console.WriteError("Strings don't match");
                return true;
            }

            return false;
        }
    }
}