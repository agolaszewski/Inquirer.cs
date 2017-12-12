using System;
using InquirerCS.Interfaces;

namespace InquirerCS.Components
{
    public class DisplayErrorCompnent : IDisplayErrorComponent
    {
        public void Render(string errorMessage)
        {
            Console.Clear();
            AppConsole2.WriteError(errorMessage);
            Console.ReadKey();
        }
    }
}
