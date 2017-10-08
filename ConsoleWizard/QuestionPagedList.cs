using System;
using System.Collections.Generic;
using System.Linq;

namespace ConsoleWizard
{
    public class QuestionPagedList<T> : QuestionList<T>
    {
        private List<T> _pageChoices = new List<T>();

        private int _skipChoices = 0;

        public QuestionPagedList(QuestionList<T> question) : base(question.Message)
        {
            ValidatationFn = question.ValidatationFn;
            ParseFn = question.ParseFn;
            DisplayQuestionAnswersFn = question.DisplayQuestionAnswersFn;
            Choices = question.Choices;
        }

        public int PageSize { get; internal set; }

= 0;

        public override T Prompt()
        {
            Console.Clear();

            bool tryAgain = true;
            T answer = DefaultValue;
            _pageChoices = Choices.Skip(_skipChoices).Take(PageSize).ToList();

            while (tryAgain)
            {
                DisplayQuestion();

                Console.WriteLine();
                Console.WriteLine();

                DisplayChoices();

                Console.CursorVisible = false;

                int boundryTop = Console.CursorTop - _pageChoices.Count;
                int boundryBottom = boundryTop + _pageChoices.Count - 1;

                Console.SetCursorPosition(0, boundryTop);
                Console.Write("→");
                Console.SetCursorPosition(0, boundryTop);

                while (true)
                {
                    int y = Console.CursorTop;
                    var key = Console.ReadKey().Key;

                    Console.SetCursorPosition(0, y);
                    ConsoleHelper.Write("  " + DisplayQuestionAnswersFn(y - boundryTop, _pageChoices[y - boundryTop]));
                    Console.SetCursorPosition(0, y);

                    switch (key)
                    {
                        case (ConsoleKey.UpArrow):
                            {
                                if (y > boundryTop)
                                {
                                    y -= 1;
                                }
                                else
                                {
                                    if (_skipChoices - PageSize >= 0)
                                    {
                                        _skipChoices -= PageSize;
                                        return Prompt();
                                    }
                                }

                                break;
                            }

                        case (ConsoleKey.DownArrow):
                            {
                                if (y < boundryBottom)
                                {
                                    y += 1;
                                }
                                else
                                {
                                    if (_skipChoices + PageSize < Choices.Count)
                                    {
                                        _skipChoices += PageSize;
                                        return Prompt();
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

                    Console.SetCursorPosition(0, y);
                    ConsoleHelper.Write("  " + DisplayQuestionAnswersFn(y - boundryTop, _pageChoices[y - boundryTop]), ConsoleColor.DarkYellow);
                    Console.SetCursorPosition(0, y);
                    Console.Write("→");
                    Console.SetCursorPosition(0, y);
                }
            }

            Answer = answer;
            Console.WriteLine();
            return answer;
        }

        private void DisplayChoices()
        {
            for (int i = 0; i < _pageChoices.Count; i++)
            {
                ConsoleHelper.WriteLine("  " + DisplayQuestionAnswersFn(i + 1, _pageChoices[i]));
            }
        }
    }
}
