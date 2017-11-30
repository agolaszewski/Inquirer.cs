using System;
using InquirerCS.Interfaces;

namespace InquirerCS.Components
{
    public class DisplayErrorCompnent : IDisplayErrorComponent
    {
        public void Render(string errorMessage)
        {
            Console.Clear();
            ConsoleHelper.WriteError(errorMessage);
            Console.ReadKey();
        }
    }
}
