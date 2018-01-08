using System;
using System.Collections.Generic;
using System.Linq;

namespace InquirerCS
{
    public class InquirerMenu
    {
        private string _header;

        private Inquirer _inquirer;

        private List<Tuple<string, Action>> _options = new List<Tuple<string, Action>>();

        private IConsole _console = new AppConsole();

        internal InquirerMenu(string header, Inquirer inquirer)
        {
            _header = header;
            _inquirer = inquirer;
        }

        public InquirerMenu AddOption(string description, Action option)
        {
            _options.Add(new Tuple<string, Action>(description, () => { option.Invoke(); Prompt(); }));
            return this;
        }

        public void Prompt()
        {
            if (!_options.Any(item => item.Item1 == "Exit"))
            {
                _options.Add(new Tuple<string, Action>("Exit", () => { return; }));
            }

            _console.Clear();
            _console.WriteLine(_header + " :");
            _console.WriteLine();

            _console.WriteLine("  " + DisplayChoice(0), ConsoleColor.DarkYellow);
            for (int i = 1; i < _options.Count; i++)
            {
                _console.WriteLine("  " + DisplayChoice(i));
            }

            Console.CursorVisible = false;

            int boundryTop = _console.CursorTop - _options.Count;
            int boundryBottom = boundryTop + _options.Count - 1;

            _console.PositionWrite("→", 0, boundryTop);

            bool move = true;
            while (move)
            {
                int y = _console.CursorTop;

                bool isCanceled = false;
                var key = _console.ReadKey(out isCanceled);
                if (isCanceled)
                {
                    if (_inquirer.History.Count > 1)
                    {
                        _inquirer.History.Pop();
                        _inquirer.Next(_inquirer.History.Pop());
                    }
                    else
                    {
                        _inquirer.Next(_inquirer.History.Pop());
                    }

                    return;
                }

                _console.SetCursorPosition(0, y);
                _console.Write("  " + DisplayChoice(y - boundryTop));
                _console.SetCursorPosition(0, y);

                switch (key.Key)
                {
                    case (ConsoleKey.UpArrow):
                        {
                            if (y > boundryTop)
                            {
                                y -= 1;
                            }

                            break;
                        }

                    case (ConsoleKey.DownArrow):
                        {
                            if (y < boundryBottom)
                            {
                                y += 1;
                            }

                            break;
                        }

                    case (ConsoleKey.Enter):
                        {
                            Console.CursorVisible = true;
                            var answer = _options[_console.CursorTop - boundryTop];
                            move = false;
                            _inquirer.Next(answer.Item2);

                            return;
                        }
                }

                _console.PositionWrite("  " + DisplayChoice(y - boundryTop), 0, y, ConsoleColor.DarkYellow);
                _console.PositionWrite("→", 0, y);
                Console.SetCursorPosition(0, y);
            }
        }

        private string DisplayChoice(int index)
        {
            return $"{_options[index].Item1}";
        }
    }
}