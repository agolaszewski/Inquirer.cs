using System;
using System.Collections.Generic;

namespace ConsoleWizard
{
    public class QuestionCheckbox<TList, T> : QuestionMultipleListBase<TList, T> where TList : List<T>, new()
    {
        internal QuestionCheckbox(string question) : base(question)
        {
        }

        internal Func<int, T, string> DisplayQuestionAnswersFn { get; set; }

        internal Func<int, T> ParseFn { get; set; } = v => { return default(T); };

        internal Func<int, bool> ValidatationFn { get; set; } = v => { return true; };

        internal override TList Prompt()
        {
            bool tryAgain = true;
            TList answer = DefaultValue;

            while (tryAgain)
            {
                DisplayQuestion();

                Console.WriteLine();
                Console.WriteLine();

                Console.CursorVisible = false;

                int boundryTop = Console.CursorTop;
                for (int i = 0; i < Choices.Count; i++)
                {
                    Console.SetCursorPosition(2, i + boundryTop);
                    if (Selected[i])
                    {
                        Console.Write("*");
                    }
                    else
                    {
                        Console.Write(" ");
                    }

                    Console.SetCursorPosition(4, i + boundryTop);
                    ConsoleHelper.WriteLine(DisplayQuestionAnswersFn(i + 1, Choices[i]));
                }

                boundryTop = Console.CursorTop - Choices.Count;
                int boundryBottom = boundryTop + Choices.Count - 1;

                Console.SetCursorPosition(0, boundryTop);
                Console.Write("→");
                Console.SetCursorPosition(0, boundryTop);

                bool move = true;
                while (move)
                {
                    int y = Console.CursorTop;
                    var key = Console.ReadKey().Key;

                    Console.SetCursorPosition(2, y);
                    if (Selected[y - boundryTop])
                    {
                        Console.Write("*");
                    }
                    else
                    {
                        Console.Write(" ");
                    }

                    Console.SetCursorPosition(4, y);
                    ConsoleHelper.Write(DisplayQuestionAnswersFn(y - boundryTop, Choices[y - boundryTop]));

                    switch (key)
                    {
                        case (ConsoleKey.RightArrow):
                            {
                                Selected[y - boundryTop] = !Selected[y - boundryTop];

                                Console.SetCursorPosition(2, y);
                                if (Selected[y - boundryTop])
                                {
                                    Console.Write("*");
                                }
                                else
                                {
                                    Console.Write(" ");
                                }

                                Console.SetCursorPosition(2, y);
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
                                var result = new TList();
                                for (int i = 0; i < Selected.Length; i++)
                                {
                                    if (Selected[i])
                                    {
                                        result.Add(Choices[i]);
                                    }
                                }

                                answer = result;
                                move = false;
                                break;
                            }
                    }

                    Console.SetCursorPosition(0, y);
                    Console.Write("→");
                    Console.SetCursorPosition(0, y);

                    Console.SetCursorPosition(4, y);
                    ConsoleHelper.Write(DisplayQuestionAnswersFn(y - boundryTop, Choices[y - boundryTop]), ConsoleColor.DarkYellow);
                    Console.SetCursorPosition(0, y);
                }

                tryAgain = Confirm(answer);
            }

            Console.WriteLine();
            return answer;
        }
    }
}