using System;
using System.Linq;

namespace ConsoleWizard
{
    public class QuestionRawList<TResult> : QuestionListBase<TResult> where TResult : IComparable
    {
        internal QuestionRawList(string question) : base(question)
        {
        }

        public Func<int, TResult> ParseFn { get; set; } = v => { return default(TResult); };

        public Func<int, bool> ValidatationFn { get; set; } = v => { return true; };

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

        public QuestionRawList<TResult> WithDefaultValue<T>(T defaultValue) where T : IComparable
        {
            if (Choices.Where(x => x.CompareTo(defaultValue) == 0).Any())
            {
                var index = Choices.Select((v, i) => new { Value = v, Index = i }).First(x => x.Value.CompareTo(defaultValue) == 0).Index;
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
            }

            Console.WriteLine();
            return answer;
        }

        protected string DisplayChoice(int index, TResult choice)
        {
            return $"[{index}] {choice}";
        }
    }
}