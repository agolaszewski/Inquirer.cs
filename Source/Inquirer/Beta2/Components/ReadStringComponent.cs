using System;
using System.Text;
using InquirerCS.Beta2.Interfaces;

namespace InquirerCS.Beta2.Components
{
    public class ReadStringComponent : IWaitForInputComponent<string>
    {
        private StringBuilder _sb = new StringBuilder();

        public ReadStringComponent()
        {
        }

        public string WaitForInput()
        {
            return Console.ReadLine();
        }
    }
}