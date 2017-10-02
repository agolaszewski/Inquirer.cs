using System;

namespace ConsoleWizard
{
    public class QuestionPagedList<T> : QuestionList<T>
    {
        public int PageSize { get; internal set; } = 0;
        private int _skipChoices = 0;

        public QuestionPagedList(QuestionList<T> question) : base(question.Message)
        {
            ValidatationFn = question.ValidatationFn;
            ParseFn = question.ParseFn;
            DisplayQuestionAnswersFn = question.DisplayQuestionAnswersFn;
            Choices = question.Choices;
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

                for (int i = 0; i < Choices.Count; i++)
                {
                    ConsoleHelper.WriteLine("  " + DisplayQuestionAnswersFn(i + 1, Choices[i]));
                }

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
                        case (ConsoleKey.LeftArrow):
                            {
                                break;
                            }
                        case (ConsoleKey.RightArrow):
                            {
                                break;
                            }
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
                ConsoleHelper.WriteLine(DisplayQuestionAnswersFn(i + 1, Choices[i]));
            }

            if (max != Choices.Count)
            {
                ConsoleHelper.WriteLine("[→] Next Page");
            }
        }
    }
}