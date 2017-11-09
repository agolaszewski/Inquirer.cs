using System;
using System.Collections.Generic;
using System.Linq;

namespace InquirerCS
{
    public class QuestionCheckbox<TList, TResult> : QuestionMultipleListBase<TList, TResult> where TList : List<TResult>, new()
    {
        internal QuestionCheckbox(string question) : base(question)
        {
        }

        internal Func<int, TResult> ParseFn { get; set; } = answer => { return default(TResult); };

        internal Func<TList, bool> ValidatationFn { get; set; } = answer => { return true; };

        internal string ErrorMessage { get; set; }

        public QuestionCheckbox<TList, TResult> WithValidatation(Func<TResult, string> fn, string errorMessage)
        {
            ConvertToStringFn = fn;
            ErrorMessage = errorMessage;
            return this;
        }

        public QuestionCheckbox<TList, TResult> ConvertToString(Func<TResult, string> fn)
        {
            ConvertToStringFn = fn;
            return this;
        }

        public QuestionCheckbox<TList, TResult> Parse(Func<int, TResult> fn)
        {
            ParseFn = fn;
            return this;
        }

        public QuestionCheckbox<TList, TResult> WithConfirmation()
        {
            HasConfirmation = true;
            return this;
        }

        public QuestionCheckbox<TList, TResult> WithDefaultValue(TList defaultValue, Func<TResult, TResult, int> compareFn = null)
        {
            if ((typeof(TResult) is IComparable || typeof(TResult).IsEnum || typeof(TResult).IsValueType) && compareFn == null)
            {
                compareFn = (l, r) =>
                {
                    var l1 = l as IComparable;
                    var r1 = r as IComparable;
                    return l1.CompareTo(r1);
                };
            }
            else if (compareFn == null)
            {
                throw new Exception("compareFn not defined");
            }

            DefaultValue = defaultValue;
            foreach (var value in defaultValue)
            {
                if (Choices.Where(item => compareFn(item, value) == 0).Any())
                {
                    var index = Choices.Select((answer, i) => new { Value = answer, Index = i }).First(x => compareFn(x.Value, value) == 0).Index;
                    Selected[index] = true;
                }
                else
                {
                    throw new Exception("No default values in choices");
                }
            }

            HasDefaultValue = true;
            return this;
        }

        public QuestionCheckbox<TList, TResult> WithDefaultValue(TResult defaultValue, Func<TResult, TResult, int> compareFn = null)
        {
            if ((typeof(TResult) is IComparable || typeof(TResult).IsEnum || typeof(TResult).IsValueType) && compareFn == null)
            {
                compareFn = (l, r) =>
                {
                    var l1 = l as IComparable;
                    var r1 = r as IComparable;
                    return l1.CompareTo(r1);
                };
            }
            else if (compareFn == null)
            {
                throw new Exception("compareFn not defined");
            }

            DefaultValue = new TList { defaultValue };
            if (Choices.Where(item => compareFn(item, defaultValue) == 0).Any())
            {
                var index = Choices.Select((answer, i) => new { Value = answer, Index = i }).First(x => compareFn(x.Value, defaultValue) == 0).Index;
                Selected[index] = true;
            }
            else
            {
                throw new Exception("No default values in choices");
            }

            HasDefaultValue = true;
            return this;
        }

        internal override TList Prompt()
        {
            int _boundryBottom;
            int _boundryTop;

            bool tryAgain = true;
            TList answer = DefaultValue;

            while (tryAgain)
            {
                DisplayQuestion();

                ConsoleHelper.WriteLine();
                ConsoleHelper.WriteLine();
                Console.CursorVisible = false;

                _boundryTop = Console.CursorTop;

                for (int i = 0; i < Choices.Count; i++)
                {
                    DisplayCheckbox(i, 2, i + _boundryTop);
                    ConsoleHelper.PositionWriteLine(DisplayChoice(i), 4, i + _boundryTop);
                }

                _boundryBottom = _boundryTop + Choices.Count - 1;

                ConsoleHelper.PositionWrite("→", 0, _boundryTop);
                ConsoleHelper.PositionWrite(DisplayChoice(0), 4, _boundryTop, ConsoleColor.DarkYellow);

                bool move = true;
                while (move)
                {
                    int y = Console.CursorTop;

                    bool isCanceled = false;
                    var key = ConsoleHelper.ReadKey(out isCanceled);
                    if (isCanceled)
                    {
                        IsCanceled = isCanceled;
                        return default(TList);
                    }

                    DisplayCheckbox(y - _boundryTop, 2, y);
                    ConsoleHelper.PositionWrite(DisplayChoice(y - _boundryTop), 4, y);

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

                                if (ValidatationFn(selectedChoices))
                                {
                                    answer = selectedChoices;
                                    move = false;
                                    break;
                                }
                                else
                                {
                                    ConsoleHelper.WriteError(ErrorMessage);
                                    break;
                                }
                            }
                    }

                    ConsoleHelper.PositionWrite(" ", 0, y - 1);
                    ConsoleHelper.PositionWrite("→", 0, y);
                    ConsoleHelper.PositionWrite(DisplayChoice(y - _boundryTop), 4, y, ConsoleColor.DarkYellow);
                    Console.SetCursorPosition(0, y);
                }

                var answerNames = answer.Select(x => ConvertToStringFn(x)).ToList();
                tryAgain = Confirm(string.Join(",", answerNames));
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

        private string DisplayChoice(int index)
        {
            return $"{ConvertToStringFn(Choices[index])}";
        }
    }
}