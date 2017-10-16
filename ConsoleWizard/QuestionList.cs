using System;
using ConsoleWizard.Components;

namespace ConsoleWizard
{
    public class QuestionList<T> : QuestionListBase<T>, IConvertToResult<int, T>, IValidation<int>
    {
        internal QuestionList(string question) : base(question)
        {
        }

        public Func<int, T> ParseFn { get; set; } = v => { return default(T); };

        public Func<int, bool> ValidatationFn { get; set; } = v => { return true; };

        internal Func<int, T, string> ChoicesDisplayFn { get; set; }

        internal override T Prompt()
        {
            bool tryAgain = true;
            T answer = DefaultValue;

            while (tryAgain)
            {
                DisplayQuestion(ToStringFn(answer));

                Console.WriteLine();
                Console.WriteLine();

                for (int i = 0; i < Choices.Count; i++)
                {
                    ConsoleHelper.WriteLine("  " + ChoicesDisplayFn(i + 1, Choices[i]));
                }

                Console.CursorVisible = false;

                int boundryTop = Console.CursorTop - Choices.Count;
                int boundryBottom = boundryTop + Choices.Count - 1;

                ConsoleHelper.PositionWrite("→", 0, boundryTop);

                bool move = true;
                while (move)
                {
                    int y = Console.CursorTop;

                    bool isCanceled = false;
                    var key = ConsoleHelper.ReadKey(out isCanceled);
                    if (isCanceled)
                    {
                        IsCanceled = isCanceled;
                        return default(T);
                    }

                    Console.SetCursorPosition(0, y);
                    ConsoleHelper.Write("  " + ChoicesDisplayFn(y - boundryTop, Choices[y - boundryTop]));
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

                    ConsoleHelper.PositionWrite("  " + ChoicesDisplayFn(y - boundryTop, Choices[y - boundryTop]), 0, y, ConsoleColor.DarkYellow);
                    ConsoleHelper.PositionWrite("→", 0, y);
                    Console.SetCursorPosition(0, y);
                }

                tryAgain = Confirm(ToStringFn(answer));
            }

            Console.WriteLine();
            return answer;
        }
    }
}
