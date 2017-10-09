using System;
using System.Collections.Generic;

namespace ConsoleWizard
{
    public class QuestionCheckbox<TList, TResult> : QuestionMultipleListBase<TList, TResult> where TList : List<TResult>, new()
    {
        private int _boundryBottom;

        private int _boundryTop;

        internal QuestionCheckbox(string question) : base(question)
        {
        }

        internal Func<int, TResult, string> ChoicesDisplayFn { get; set; }

        internal Func<int, TResult> ParseFn { get; set; } = v => { return default(TResult); };

        internal Func<int, bool> ValidatationFn { get; set; } = v => { return true; };

        internal override TList Prompt()
        {
            bool tryAgain = true;
            TList answer = DefaultValue;

            while (tryAgain)
            {
                DisplayQuestion();

                ConsoleHelper.WriteLine();
                ConsoleHelper.WriteLine();
                Console.CursorVisible = false;

                _boundryTop = Console.CursorTop;
                DisplayChoices();

                _boundryBottom = _boundryTop + Choices.Count - 1;

                ConsoleHelper.PositionWrite("→", 0, _boundryTop);
                ConsoleHelper.PositionWrite(ChoicesDisplayFn(0, Choices[0]), 4, _boundryTop, ConsoleColor.DarkYellow);

                bool move = true;
                while (move)
                {
                    int y = Console.CursorTop;
                    var key = Console.ReadKey().Key;

                    DisplayCheckbox(y - _boundryTop, 2, y);
                    ConsoleHelper.PositionWrite(ChoicesDisplayFn(y - _boundryTop, Choices[y - _boundryTop]), 4, y);

                    switch (key)
                    {
                        case (ConsoleKey.RightArrow):
                            {
                                Selected[y - _boundryTop] = !Selected[y - _boundryTop];
                                DisplayCheckbox(y - _boundryTop, 2, y);

                                break;
                            }

                        case (ConsoleKey.UpArrow):
                            {
                                if (y > _boundryTop)
                                {
                                    y -= 1;
                                }

                                break;
                            }

                        case (ConsoleKey.DownArrow):
                            {
                                if (y < _boundryBottom)
                                {
                                    y += 1;
                                }

                                break;
                            }

                        case (ConsoleKey.Enter):
                            {
                                Console.CursorVisible = true;
                                var selectedChoices = new TList();
                                for (int i = 0; i < Selected.Length; i++)
                                {
                                    if (Selected[i])
                                    {
                                        selectedChoices.Add(Choices[i]);
                                    }
                                }

                                answer = selectedChoices;
                                move = false;
                                break;
                            }
                    }

                    ConsoleHelper.PositionWrite(" ", 0, y - 1);
                    ConsoleHelper.PositionWrite("→", 0, y);
                    ConsoleHelper.PositionWrite(ChoicesDisplayFn(y - _boundryTop, Choices[y - _boundryTop]), 4, y, ConsoleColor.DarkYellow);
                    Console.SetCursorPosition(0, y);
                }

                tryAgain = Confirm(answer);
            }

            Console.WriteLine();
            return answer;
        }

        private void DisplayCheckbox(int selectedIndex, int x, int y)
        {
            if (Selected[selectedIndex])
            {
                ConsoleHelper.PositionWrite("*", x, y);
            }
            else
            {
                ConsoleHelper.PositionWrite(" ", x, y);
            }
        }

        private void DisplayChoices()
        {
            for (int i = 0; i < Choices.Count; i++)
            {
                DisplayCheckbox(i, 2, i + _boundryTop);
                ConsoleHelper.PositionWriteLine(ChoicesDisplayFn(i + 1, Choices[i]), 4, i + _boundryTop);
            }
        }
    }
}
