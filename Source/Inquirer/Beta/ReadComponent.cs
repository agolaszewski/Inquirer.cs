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