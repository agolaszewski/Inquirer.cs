using System;

namespace ConsoleWizard.Events
{
    public class SpecialKeyReturnedEvent : IEvent
    {
        public SpecialKeyReturnedEvent(ConsoleKey consoleKey)
        {
            ConsoleKey = consoleKey;
        }

        public ConsoleKey ConsoleKey { get; private set; }
    }
}