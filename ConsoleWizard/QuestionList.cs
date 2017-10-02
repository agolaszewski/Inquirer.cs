using System;
using System.Collections.Generic;

namespace ConsoleWizard
{
    public class QuestionList<T> : QuestionBase<T>
    {
        public Func<int, bool> ValidatationFn { get; set; } = v => { return true; };
        public Func<int, T> ParseFn { get; set; } = v => { return default(T); };
        public Func<int, T, string> DisplayQuestionAnswersFn { get; set; }
        public List<T> Choices { get; internal set; }
        public int PageSize { get; internal set; } = 0;

        private int _skipChoices = 0;

        public QuestionRawList(string question) : base(question)
        {

        }

        public override T Prompt()
        {
            bool tryAgain = true;
            T answer = DefaultValue;

            while (tryAgain)
            {
                DisplayQuestion();

                Console.WriteLine();
                Console.WriteLine();

                DisplayChoices();

                Console.WriteLine();
                ConsoleHelper.Write("Answer: ");

                string result = string.Empty;
                ConsoleKey key;
                do
                {
                    key = Console.ReadKey().Key;
                    if (key == ConsoleKey.LeftArrow)
                    {
                        _skipChoices = MathHelper.Clamp(_skipChoices - PageSize, 0, Choices.Count);
                        if (_skipChoices - PageSize >= 0)
                        {
                            return Prompt();
                        }
                    }
                    else if (key == ConsoleKey.RightArrow)
                    {
                        _skipChoices = MathHelper.Clamp(_skipChoices + PageSize, 0, Choices.Count);
                        if (_skipChoices != Choices.Count)
                        {
                            return Prompt();
                        }
                    }
                    else if (key != ConsoleKey.Enter)
                    {
                        result += (char)key;
                    }
                } while (key != ConsoleKey.Enter);

                Console.CursorVisible = false;

                int boundryTop = Console.CursorTop - Choices.Count;
                int boundryBottom = boundryTop + Choices.Count - 1;

                Console.SetCursorPosition(0, boundryTop);
                Console.Write("→");
                Console.SetCursorPosition(0, boundryTop);

                while (true)
                {
                    int y = Console.CursorTop;
                    var key = Console.ReadKey().Key;

                    Console.SetCursorPosition(0, y);
                    ConsoleHelper.Write("  " + DisplayQuestionAnswersFn(y - boundryTop, Choices[y - boundryTop]));
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
                                return Choices[Console.CursorTop - boundryTop];
                            }
                    }

                    Console.SetCursorPosition(0, y);
                    ConsoleHelper.Write("  " + DisplayQuestionAnswersFn(y - boundryTop, Choices[y - boundryTop]), ConsoleColor.DarkYellow);
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
            if (_skipChoices != 0)
            {
                ConsoleHelper.WriteLine("[←] Previous Page");
            }

            int max = MathHelper.Clamp(_skipChoices + PageSize, 0, Choices.Count);

            for (int i = _skipChoices; i < max; i++)
            {
                DisplayQuestionAnswersFn(i + 1, Choices[i]);
            }

            if (PageSize != 0 && max != Choices.Count)
            {
                ConsoleHelper.WriteLine("[→] Next Page");
            }
        }
    }
}