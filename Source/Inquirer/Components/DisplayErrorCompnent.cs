using System;
using InquirerCS.Interfaces;

namespace InquirerCS.Components
{
    public class DisplayErrorCompnent : IDisplayErrorComponent
    {
        private IConsole _console;

        public DisplayErrorCompnent(IConsole console)
        {
            _console = console;
        }

        public void Render(string errorMessage)
        {
            Console.Clear();
            _console.WriteError(errorMessage);
            Console.ReadKey();
        }
    }
}
