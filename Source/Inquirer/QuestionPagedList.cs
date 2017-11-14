using System;
using System.Collections.Generic;
using System.Linq;

namespace InquirerCS
{
    public class QuestionPagedList<TResult> : QuestionList<TResult>
    {
        private List<TResult> _pageChoices = new List<TResult>();

        private int _skipChoices = 0;

        public QuestionPagedList(QuestionList<TResult> questionList, int pageSize) : base(questionList)
        {
            PageSize = pageSize;
        }

        internal int PageSize { get; set; } = 0;

        internal override TResult Prompt()
        {
            return Prompt(Console.CursorTop);
        }

        protected override string DisplayChoice(int index)
        {
            return $"{ConvertToStringFn(_pageChoices[index])}";
        }

        private TResult Prompt(int cursorPosition)
        {
            int y = cursorPosition;

            Console.Clear();

            bool tryAgain = true;
            TResult answer = DefaultValue;
            _pageChoices = Choices.Skip(_skipChoices).Take(PageSize).ToList();

            while (tryAgain)
            {
                DisplayQuestion();

                Console.WriteLine();
                Console.WriteLine();

                for (int i = 0; i < _pageChoices.Count; i++)
                {
                    ConsoleHelper.PositionWriteLine(DisplayChoice(i), 2, Console.CursorTop);
                }

                ConsoleHelper.PositionWrite($"Page {Math.Ceiling(((double)_skipChoices / (double)PageSize) + 1)} of {Math.Ceiling((double)Choices.Count / (double)PageSize)}", y: Console.CursorTop + 1);
                Console.SetCursorPosition(0, Console.CursorTop - 1);

                Console.CursorVisible = false;

                int boundryTop = Console.CursorTop - _pageChoices.Count;
                int boundryBottom = boundryTop + _pageChoices.Count - 1;

                ConsoleHelper.PositionWrite("→", 0, y);
                ConsoleHelper.PositionWrite(DisplayChoice(y - boundryTop), 2, y, ConsoleColor.DarkYellow);

                while (true)
                {
                    bool isCanceled = false;
                    var key = ConsoleHelper.ReadKey(out isCanceled);
                    if (isCanceled)
                    {
                        IsCanceled = isCanceled;
                        return default(TResult);
                    }

                    switch (key)
                    {
                        case (ConsoleKey.LeftArrow):
                            {
                                if (_skipChoices - PageSize >= 0)
                                {
                                    _skipChoices -= PageSize;
                                    return Prompt(PageSize - 1);
                                }

                                break;
                            }

                        case (ConsoleKey.RightArrow):
                            {
                                if (_skipChoices + PageSize < Choices.Count)
                                {
                                    _skipChoices += PageSize;
                                    return Prompt(PageSize - 1);
                                }

                                break;
                            }

                        case (ConsoleKey.UpArrow):
                            {
                                if (y > boundryTop)
                                {
                                    ConsoleHelper.PositionWrite(" ", 0, y);
                                    ConsoleHelper.PositionWrite(DisplayChoice(y - boundryTop), 2, y);
                                    y -= 1;
                                }
                                else
                                {
                                    if (_skipChoices - PageSize >= 0)
                                    {
                                        _skipChoices -= PageSize;
                                        return Prompt(PageSize + 1);
                                    }
                                }

                                break;
                            }

                        case (ConsoleKey.DownArrow):
                            {
                                if (y < boundryBottom)
                                {
                                    ConsoleHelper.PositionWrite(" ", 0, y);
                                    ConsoleHelper.PositionWrite(DisplayChoice(y - boundryTop), 2, y);
                                    y += 1;
                                }
                                else
                                {
                                    if (_skipChoices + PageSize < Choices.Count)
                                    {
                                        _skipChoices += PageSize;
                                        return Prompt(PageSize - 1);
                                    }
                                }

                                break;
                            }

                        case (ConsoleKey.Enter):
                            {
                                Console.CursorVisible = true;

                                return _pageChoices[Console.CursorTop - boundryTop];
                            }
                    }

                    ConsoleHelper.PositionWrite("→", 0, y);
                    ConsoleHelper.PositionWrite(DisplayChoice(y - boundryTop), 2, y, ConsoleColor.DarkYellow);
                }
            }

            Console.WriteLine();
            return answer;
        }
    }
}
