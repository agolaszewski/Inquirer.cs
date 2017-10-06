using System;

namespace ConsoleWizard
{
    public class QuestionList<T> : QuestionListBase<T>
    {
        public Func<int, bool> ValidatationFn { get; set; } = v => { return true; };
        public Func<int, T> ParseFn { get; set; } = v => { return default(T); };
        public Func<int, T, string> DisplayQuestionAnswersFn { get; set; }

        public QuestionList(string question) : base(question)
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

                bool move = true;
                while (move)
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
                                answer = Choices[Console.CursorTop - boundryTop];
                                move = false;
                                break;
                            }
                    }

                    Console.SetCursorPosition(0, y);
                    ConsoleHelper.Write("  " + DisplayQuestionAnswersFn(y - boundryTop, Choices[y - boundryTop]), ConsoleColor.DarkYellow);
                    Console.SetCursorPosition(0, y);
                    Console.Write("→");
                    Console.SetCursorPosition(0, y);
                }
                tryAgain = Confirm(answer);
            }
            Answer = answer;
            Console.WriteLine();
            return answer;
        }
    }
}