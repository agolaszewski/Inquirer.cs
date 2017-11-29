using System;
using InquirerCS.Beta2.Interfaces;

namespace InquirerCS.Beta2.Components
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