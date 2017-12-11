using System;
using System.Linq;
using InquirerCS.Interfaces;

namespace InquirerCS.Components
{
    public class StringOrKeyInputComponent : IWaitForInputComponent<StringOrKey>
    {
        private ConsoleKey[] _intteruptedKeys;

        public StringOrKeyInputComponent(params ConsoleKey[] intteruptedKeys)
        {
            _intteruptedKeys = intteruptedKeys;
        }

        public StringOrKeyInputComponent()
        {
            _intteruptedKeys = Enum.GetValues(typeof(ConsoleKey)).Cast<ConsoleKey>().ToArray();
        }

        public StringOrKey WaitForInput()
        {
            ConsoleKey? intteruptedKey;
            string result = ConsoleHelper.Read(out intteruptedKey, _intteruptedKeys);
            return new StringOrKey(result, intteruptedKey);
        }
    }
}
