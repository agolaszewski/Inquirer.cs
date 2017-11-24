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
            while (true)
            {
                ConsoleKeyInfo keyPressed = Console.ReadKey();
                if (keyPressed.Key == ConsoleKey.Enter)
                {
                    return _sb.ToString();
                }

                _sb.Append(keyPressed.KeyChar);
            }
        }
    }
}