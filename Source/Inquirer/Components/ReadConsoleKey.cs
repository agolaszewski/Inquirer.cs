using System;
using InquirerCS.Interfaces;

namespace InquirerCS.Components
{
    public class ReadConsoleKey : IWaitForInputComponent<StringOrKey>
    {
        public StringOrKey WaitForInput()
        {
            var key = Console.ReadKey().Key;
            return new StringOrKey(null, key);
        }
    }
}
