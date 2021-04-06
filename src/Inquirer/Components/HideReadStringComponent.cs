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

        public Func<char, bool> AllowTypeFn { get; set; }

        public List<ConsoleKey> IntteruptedKeys { get; set; } = new List<ConsoleKey>();

        public StringOrKey WaitForInput()
        {
            Stack<char> stringBuilder = new Stack<char>();

            ConsoleKeyInfo key;
            do
            {
                key = Console.ReadKey();

                if (IntteruptedKeys.Contains(key.Key))
                {
                    return new StringOrKey(string.Join(string.Empty, stringBuilder.ToArray().Reverse()), key.Key);
                }

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
                                if (_console.CursorLeft == 0)
                                {
                                    _console.PositionWrite(" ", 0, _console.CursorTop);
                                    _console.SetCursorPosition(Console.BufferWidth - 1, _console.CursorTop - 1);
                                }
                                else
                                {
                                    _console.PositionWrite(" ", _console.CursorLeft, _console.CursorTop);
                                    _console.SetCursorPosition(_console.CursorLeft - 1, _console.CursorTop);
                                }

                                stringBuilder.Pop();
                                break;
                            }

                            _console.SetCursorPosition(_console.CursorLeft + 1, _console.CursorTop);
                            break;
                        }

                    default:
                        {
                            if (_console.CursorLeft - 1 < 0)
                            {
                                _console.PositionWrite("*", Console.BufferWidth - 1, _console.CursorTop - 1);
                            }
                            else
                            {
                                _console.PositionWrite("*", _console.CursorLeft - 1, _console.CursorTop);
                            }

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
