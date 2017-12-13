using System;
using System.Collections.Generic;
using System.Linq;
using InquirerCS.Interfaces;

namespace InquirerCS.Components
{
    internal class HideReadStringComponent : IWaitForInputComponent<StringOrKey>
    {
        private IConsole _console;

        public HideReadStringComponent(IConsole console)
        {
            _console = console;
        }

        public StringOrKey WaitForInput()
        {
            Stack<char> stringBuilder = new Stack<char>();

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
                                _console.PositionWrite(" ", _console.CursorLeft, _console.CursorTop);
                                _console.SetCursorPosition(_console.CursorLeft - 1, _console.CursorTop);
                                stringBuilder.Pop();
                                break;
                            }

                            _console.SetCursorPosition(_console.CursorLeft + 1, _console.CursorTop);
                            break;
                        }

                    default:
                        {
                            _console.PositionWrite("*", _console.CursorLeft - 1, _console.CursorTop);
                            stringBuilder.Push(key.KeyChar);
                            break;
                        }
                }
            }
            while (key.Key != ConsoleKey.Enter);

            return new StringOrKey(string.Join(string.Empty, stringBuilder.ToArray().Reverse()), null);
        }
    }
}
