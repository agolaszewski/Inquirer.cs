using System;
using InquirerCS.Interfaces;

namespace InquirerCS.Components
{
    public class ReadStringComponent : IWaitForInputComponent<string>
    {
        public ReadStringComponent()
        {
        }

        public string WaitForInput()
        {
            return Console.ReadLine();
        }
    }
}
