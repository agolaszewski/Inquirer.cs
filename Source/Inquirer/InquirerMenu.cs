using System;
using System.Collections.Generic;
using System.Linq;

namespace InquirerCS
{
    public class InquirerMenu
    {
        private string _header;

        private List<Tuple<string, Node>> _options = new List<Tuple<string, Node>>();

        private IConsole _console = new AppConsole();
        private Node _root;

        internal InquirerMenu(string header)
        {
            _header = header;

            _root = new Node(null, Node.CurrentNode);
            _root.Then(() => { Prompt(); });
        }

        public InquirerMenu AddOption(string description, Action option)
        {
            var node = new Node(_root);
            node.Then(() => { option.Invoke(); Prompt(); });

            _options.Add(new Tuple<string, Node>(description, node));
            return this;
        }

        public void Prompt()
        {
            if (!_options.Any(item => item.Item1 == "Exit"))
            {
                _options.Add(new Tuple<string, Node>("Exit", new Node(_root)));
            }

            _console.Clear();

            if (_header != null)
            {
                _console.WriteLine(_header + " :");
                _console.WriteLine();
            }

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
                ////if (isCanceled)
                ////{
                ////    if (Node.CurrentNode.Parent != null)
                ////    {
                ////        Node.CurrentNode.Next = null;
                ////        Node.CurrentNode.Parent.Go();
                ////    }

                ////    if (Node.CurrentNode.Sibling != null)
                ////    {
                ////        Node.CurrentNode.Next = null;
                ////        Node.CurrentNode.Sibling.Go();
                ////    }
                ////}

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
                            answer.Item2.Task();
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