using System;
using InquirerCS.Interfaces;

namespace InquirerCS.Components
{
    public class ReadConsoleKey : IWaitForInputComponent<ConsoleKey>
    {
        public ConsoleKey WaitForInput()
        {
            return Console.ReadKey().Key;
        }
    }
}
