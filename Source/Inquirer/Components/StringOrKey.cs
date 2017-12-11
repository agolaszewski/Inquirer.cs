using System;

namespace InquirerCS.Components
{
    public class StringOrKey
    {
        public StringOrKey(string value, ConsoleKey? interruptKey)
        {
            Value = value;
            InterruptKey = interruptKey;
        }

        public ConsoleKey? InterruptKey { get; }

        public string Value { get; }
    }
}
