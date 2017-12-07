using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
