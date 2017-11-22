using System;
using System.Text;

namespace InquirerCS.Beta
{
    public class ReadComponent : IReadInputComponent<string>
    {
        private StringBuilder _sb = new StringBuilder();

        public ReadComponent()
        {
        }

        public string WaitForInput()
        {
            while (true)
            {
                ConsoleKey keyPressed = Console.ReadKey().Key;
                if (keyPressed == ConsoleKey.Enter)
                {
                    return _sb.ToString();
                }

                _sb.Append(keyPressed);
            }
        }
    }
}