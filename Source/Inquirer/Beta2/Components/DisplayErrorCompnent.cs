using System;
using InquirerCS.Beta2.Interfaces;

namespace InquirerCS.Beta2.Components
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