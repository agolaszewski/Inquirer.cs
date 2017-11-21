using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace InquirerCS
{
    public class InquirerMenu
    {
        private string _header;

        private List<Tuple<string, Action>> _options = new List<Tuple<string, Action>>();

        public InquirerMenu(string header)
        {
            _header = header;
        }

        public InquirerMenu AddOption(string description, Action option)
        {
            _options.Add(new Tuple<string, Action>(description, option));
            return this;
        }

        public void Prompt()
        {
            if (_options.Count == 0)
            {
                throw new Exception("No options defined");
            }

            StackTrace stackTrace = new StackTrace();
            StackFrame[] stackFrames = stackTrace.GetFrames();
            StackFrame callingFrame = stackFrames[1];

            Console.Clear();
            ConsoleHelper.WriteLine(_header + " :");
            ConsoleHelper.WriteLine();

            ConsoleHelper.WriteLine("  " + DisplayChoice(0), ConsoleColor.DarkYellow);
            for (int i = 1; i < _options.Count; i++)
            {
                ConsoleHelper.WriteLine("  " + DisplayChoice(i));
            }

            Console.CursorVisible = false;

            int boundryTop = Console.CursorTop - _options.Count;
            int boundryBottom = boundryTop + _options.Count - 1;

            ConsoleHelper.PositionWrite("→", 0, boundryTop);

            bool move = true;
            while (move)
            {
                int y = Console.CursorTop;

                bool isCanceled = false;
                var key = ConsoleHelper.ReadKey(out isCanceled);
                if (isCanceled)
                {
                    Prompt();
                    return;
                }

                Console.SetCursorPosition(0, y);
                ConsoleHelper.Write("  " + DisplayChoice(y - boundryTop));
                Console.SetCursorPosition(0, y);

                switch (key)
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
                            var answer = _options[Console.CursorTop - boundryTop];
                            move = false;
                            answer.Item2();
                            return;
                        }
                }

                ConsoleHelper.PositionWrite("  " + DisplayChoice(y - boundryTop), 0, y, ConsoleColor.DarkYellow);
                ConsoleHelper.PositionWrite("→", 0, y);
                Console.SetCursorPosition(0, y);
            }
        }

        private string DisplayChoice(int index)
        {
            return $"{_options[index].Item1}";
        }
    }
}
