using System;
using System.Linq;

namespace InquirerCS
{
    public class QuestionRawList<TResult> : QuestionListBase<TResult>
    {
        internal QuestionRawList(string question) : base(question)
        {
        }

        internal string ErrorMessage { get; set; }

        internal Func<int, TResult> ParseFn { get; set; } = answer => { return default(TResult); };

        internal Func<int, bool> ValidatationFn { get; set; } = answer => { return true; };

        public QuestionRawList<TResult> ConvertToString(Func<TResult, string> fn)
        {
            ConvertToStringFn = fn;
            return this;
        }

        public QuestionRawList<TResult> Parse(Func<int, TResult> fn)
        {
            ParseFn = fn;
            return this;
        }

        public QuestionRawList<TResult> Validation(Func<int, bool> fn)
        {
            ValidatationFn = fn;
            return this;
        }

        public QuestionRawList<TResult> WithConfirmation()
        {
            HasConfirmation = true;
            return this;
        }

        public QuestionRawList<TResult> WithDefaultValue(TResult defaultValue, Func<TResult, TResult, int> compareFn = null)
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

        public QuestionRawList<TResult> WithValidatation(Func<int, bool> fn, string errorMessage)
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

                for (int i = 0; i < Choices.Count; i++)
                {
                    ConsoleHelper.WriteLine(DisplayChoice(i + 1, Choices[i]));
                }

                Console.WriteLine();
                ConsoleHelper.Write("Answer: ");

                bool isCanceled = false;
                var value = ConsoleHelper.Read(out isCanceled).ToN<int>();
                if (isCanceled)
                {
                    IsCanceled = isCanceled;
                    return default(TResult);
                }

                if (value.HasValue == false && HasDefaultValue)
                {
                    tryAgain = Confirm(ConvertToStringFn(answer));
                }
                else if (value.HasValue && ValidatationFn(value.Value))
                {
                    answer = ParseFn(value.Value);
                    tryAgain = Confirm(ConvertToStringFn(answer));
                }
                else
                {
                    ConsoleHelper.WriteError(ErrorMessage);
                }
            }

            Console.WriteLine();
            return answer;
        }

        protected string DisplayChoice(int index, TResult choice)
        {
            return $"[{index}] {ConvertToStringFn(choice)}";
        }
    }
}
