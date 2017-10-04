using System;
using System.Collections.Generic;

namespace ConsoleWizard
{
    public class QuestionCheckbox<TList, T> : QuestionMultipleListBase<TList, T> where TList : List<T>, new()
    {
        public Func<int, bool> ValidatationFn { get; set; } = v => { return true; };
        public Func<int, T> ParseFn { get; set; } = v => { return default(T); };
        public Func<int, T, string> DisplayQuestionAnswersFn { get; set; }

        public bool[] Selected { get; set; }

        public QuestionCheckbox(string question) : base(question)
        {
        }

        public override TList Prompt()
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

                while (true)
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
                                return result;
                            }
                    }

                    Console.SetCursorPosition(0, y);
                    Console.Write("→");
                    Console.SetCursorPosition(0, y);

                    Console.SetCursorPosition(4, y);
                    ConsoleHelper.Write(DisplayQuestionAnswersFn(y - boundryTop, Choices[y - boundryTop]), ConsoleColor.DarkYellow);
                    Console.SetCursorPosition(0, y);
                }
            }
            Answer = answer;
            Console.WriteLine();
            return answer;
        }
    }
}