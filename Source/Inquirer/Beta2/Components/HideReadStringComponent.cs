using System;
using System.Collections.Generic;
using System.Linq;
using InquirerCS.Beta2.Interfaces;

namespace InquirerCS.Beta2.Components
{
    internal class HideReadStringComponent : IWaitForInputComponent<string>
    {
        private Stack<char> stringBuilder = new Stack<char>();

        public string WaitForInput()
        {
            ConsoleKeyInfo key;
            do
            {
                key = Console.ReadKey();

                switch (key.Key)
                {
                    case (ConsoleKey.Enter):
                        {
                            break;
                        }

                    case (ConsoleKey.Backspace):
                        {
                            if (stringBuilder.Any())
                            {
                                ConsoleHelper.PositionWrite(" ", Console.CursorLeft, Console.CursorTop);
                                Console.SetCursorPosition(Console.CursorLeft - 1, Console.CursorTop);
                                stringBuilder.Pop();
                                break;
                            }

                            Console.SetCursorPosition(Console.CursorLeft + 1, Console.CursorTop);
                            break;
                        }

                    default:
                        {
                            ConsoleHelper.PositionWrite("*", Console.CursorLeft - 1, Console.CursorTop);
                            stringBuilder.Push(key.KeyChar);
                            break;
                        }
                }
            }
            while (key.Key != ConsoleKey.Enter);

            return string.Join(string.Empty, stringBuilder.ToArray().Reverse());
        }
    }
}