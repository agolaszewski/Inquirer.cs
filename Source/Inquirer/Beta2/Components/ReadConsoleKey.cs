using System;
using InquirerCS.Beta2.Interfaces;

namespace InquirerCS.Beta2.Components
{
    public class ReadConsoleKey : IWaitForInputComponent<ConsoleKey>
    {
        public ConsoleKey WaitForInput()
        {
            return Console.ReadKey().Key;
        }
    }
}