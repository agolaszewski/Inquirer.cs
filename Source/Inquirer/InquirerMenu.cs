using System;
using System.Collections.Generic;

namespace InquirerCS
{
    public class InquirerMenu
    {
        private string _header;

        private Inquirer _inquirer;

        private List<Tuple<string, Action>> _options = new List<Tuple<string, Action>>();

        internal InquirerMenu(string header, Inquirer inquirer)
        {
            _header = header;
            _inquirer = inquirer;
        }

        public InquirerMenu AddOption(string description, Action option)
        {
            _options.Add(new Tuple<string, Action>(description, option));
            return this;
        }

        public void Prompt()
        {
            ////if (_options.Count == 0)
            ////{
            ////    throw new Exception("No options defined");
            ////}

            ////Console.Clear();
            ////AppConsole2.WriteLine(_header + " :");
            ////AppConsole2.WriteLine();

            ////AppConsole2.WriteLine("  " + DisplayChoice(0), ConsoleColor.DarkYellow);
            ////for (int i = 1; i < _options.Count; i++)
            ////{
            ////    AppConsole2.WriteLine("  " + DisplayChoice(i));
            ////}

            ////Console.CursorVisible = false;

            ////int boundryTop = _console - _options.Count;
            ////int boundryBottom = boundryTop + _options.Count - 1;

            ////AppConsole2.PositionWrite("→", 0, boundryTop);

            ////bool move = true;
            ////while (move)
            ////{
            ////    int y = _console;

            ////    bool isCanceled = false;
            ////    var key = AppConsole2.ReadKey(out isCanceled);
            ////    if (isCanceled)
            ////    {
            ////        if (_inquirer.History.Count > 1)
            ////        {
            ////            _inquirer.History.Pop();
            ////            _inquirer.Next(_inquirer.History.Pop());
            ////        }
            ////        else
            ////        {
            ////            _inquirer.Next(_inquirer.History.Pop());
            ////        }

            ////        return;
            ////    }

            ////    Console.SetCursorPosition(0, y);
            ////    AppConsole2.Write("  " + DisplayChoice(y - boundryTop));
            ////    Console.SetCursorPosition(0, y);

            ////    switch (key)
            ////    {
            ////        case (ConsoleKey.UpArrow):
            ////            {
            ////                if (y > boundryTop)
            ////                {
            ////                    y -= 1;
            ////                }

            ////                break;
            ////            }

            ////        case (ConsoleKey.DownArrow):
            ////            {
            ////                if (y < boundryBottom)
            ////                {
            ////                    y += 1;
            ////                }

            ////                break;
            ////            }

            ////        case (ConsoleKey.Enter):
            ////            {
            ////                Console.CursorVisible = true;
            ////                var answer = _options[_console - boundryTop];
            ////                move = false;
            ////                _inquirer.Next(answer.Item2);

            ////                return;
            ////            }
            ////    }

            ////    AppConsole2.PositionWrite("  " + DisplayChoice(y - boundryTop), 0, y, ConsoleColor.DarkYellow);
            ////    AppConsole2.PositionWrite("→", 0, y);
            ////    Console.SetCursorPosition(0, y);
            ////}
        }

        private string DisplayChoice(int index)
        {
            return $"{_options[index].Item1}";
        }
    }
}
