using System;
using System.Collections.Generic;
using System.Linq;
using InquirerCS.Interfaces;

namespace InquirerCS.Components
{
    internal class StringOrKeyInputComponent : IWaitForInputComponent<StringOrKey>
    {
        private IConsole _console;

        public StringOrKeyInputComponent(IConsole console)
        {
            _console = console;
            IntteruptedKeys = Enum.GetValues(typeof(ConsoleKey)).Cast<ConsoleKey>().ToList();
        }

        public StringOrKeyInputComponent(IConsole console, Func<char, bool> allowFn = null, params ConsoleKey[] intteruptedKeys)
        {
            IntteruptedKeys = intteruptedKeys.ToList();
            _console = console;
            AllowTypeFn = allowFn ?? AllowTypeFn;
        }

        public Func<char, bool> AllowTypeFn { get; set; } = value => { return !char.IsControl(value); };

        public List<ConsoleKey> IntteruptedKeys { get; set; }

        public StringOrKey WaitForInput()
        {
            ConsoleKey? intteruptedKey;
            string result = _console.Read(out intteruptedKey, AllowTypeFn, IntteruptedKeys.ToArray());
            return new StringOrKey(result, intteruptedKey);
        }
    }
}
