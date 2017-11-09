using System;
using System.Linq;

namespace InquirerCS
{
    public class QuestionList<TResult> : QuestionListBase<TResult>
    {
        internal QuestionList(string question) : base(question)
        {
        }

        internal string ErrorMessage { get; set; }

        internal Func<int, TResult> ParseFn { get; set; } = answer => { return default(TResult); };

        internal Func<int, bool> ValidatationFn { get; set; } = answer => { return true; };

        public QuestionList<TResult> ConvertToString(Func<TResult, string> fn)
        {
            ConvertToStringFn = fn;
            return this;
        }

        public QuestionList<TResult> Parse(Func<int, TResult> fn)
        {
            ParseFn = fn;
            return this;
        }

        public QuestionList<TResult> Validation(Func<int, bool> fn)
        {
            ValidatationFn = fn;
            return this;
        }

        public QuestionList<TResult> WithConfirmation()
        {
            HasConfirmation = true;
            return this;
        }

        public QuestionList<TResult> WithDefaultValue(TResult defaultValue, Func<TResult, TResult, int> compareFn = null)
        {
            if ((typeof(TResult) is IComparable || typeof(TResult).IsEnum || typeof(TResult).IsValueType) && compareFn == null)
            {
                compareFn = (x, y) =>
                {
                    var x1 = x as IComparable;
                    var y1 = y as IComparable;
                    return x1.CompareTo(y1);
                };
            }
            else if (compareFn == null)
            {
                throw new Exception("compareFn not defined");
            }

            if (Choices.Where(x => compareFn(x, defaultValue) == 0).Any())
            {
                var index = Choices.Select((answer, i) => new { Value = answer, Index = i }).First(x => compareFn(x.Value, defaultValue) == 0).Index;
                Choices.Insert(0, Choices[index]);
                Choices.RemoveAt(index + 1);

                DefaultValue = Choices[0];
                HasDefaultValue = true;
            }
            else
            {
                throw new Exception("No default values in choices");
            }

            return this;
        }

        public QuestionList<TResult> WithValidatation(Func<int, bool> fn, string errorMessage)
        {
            ValidatationFn = fn;
            ErrorMessage = errorMessage;
            return this;
        }

        internal override TResult Prompt()
        {
            bool tryAgain = true;
            TResult answer = DefaultValue;

            while (tryAgain)
            {
                DisplayQuestion();

                Console.WriteLine();
                Console.WriteLine();

                if (Choices.Count == 0)
                {
                    ConsoleHelper.WriteError("No options to select");
                    Console.ReadKey();
                    IsCanceled = true;
                    return default(TResult);
                }

                ConsoleHelper.WriteLine("  " + DisplayChoice(0), ConsoleColor.DarkYellow);
                for (int i = 1; i < Choices.Count; i++)
                {
                    ConsoleHelper.WriteLine("  " + DisplayChoice(i));
                }

                Console.CursorVisible = false;

                int boundryTop = Console.CursorTop - Choices.Count;
                int boundryBottom = boundryTop + Choices.Count - 1;

                ConsoleHelper.PositionWrite("→", 0, boundryTop);

                bool move = true;
                do
                {
                    int y = Console.CursorTop;

                    bool isCanceled = false;
                    var key = ConsoleHelper.ReadKey(out isCanceled);
                    if (isCanceled)
                    {
                        IsCanceled = isCanceled;
                        return default(TResult);
                    }

                    Console.SetCursorPosition(0, y);
                    ConsoleHelper.Write("  " + DisplayChoice(y - boundryTop));
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

                    ConsoleHelper.PositionWrite("  " + DisplayChoice(y - boundryTop), 0, y, ConsoleColor.DarkYellow);
                    ConsoleHelper.PositionWrite("→", 0, y);
                    Console.SetCursorPosition(0, y);
                }
                while (move);

                tryAgain = Confirm(ConvertToStringFn(answer));
            }

            Console.WriteLine();
            return answer;
        }

        protected string DisplayChoice(int index)
        {
            return $"{ConvertToStringFn(Choices[index])}";
        }
    }
}
