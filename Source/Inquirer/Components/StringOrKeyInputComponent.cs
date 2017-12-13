using System;
using System.Linq;
using InquirerCS.Interfaces;

namespace InquirerCS.Components
{
    public class StringOrKeyInputComponent : IWaitForInputComponent<StringOrKey>
    {
        private IConsole _console;

        private ConsoleKey[] _intteruptedKeys;

        public StringOrKeyInputComponent(IConsole console)
        {
            _console = console;
            _intteruptedKeys = Enum.GetValues(typeof(ConsoleKey)).Cast<ConsoleKey>().ToArray();
        }

        public StringOrKeyInputComponent(IConsole console, params ConsoleKey[] intteruptedKeys)
        {
            _intteruptedKeys = intteruptedKeys;
            _console = console;
        }

        public StringOrKey WaitForInput()
        {
            ConsoleKey? intteruptedKey;
            string result = _console.Read(out intteruptedKey, _intteruptedKeys);
            return new StringOrKey(result, intteruptedKey);
        }
    }
}
