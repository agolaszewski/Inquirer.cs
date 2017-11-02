using System;

namespace ConsoleWizard
{
    public class QuestionInputKey<TResult> : QuestionSingleChoiceBase<TResult>
    {
        internal QuestionInputKey(string question) : base(question)
        {
        }

        public Func<ConsoleKey, TResult> ParseFn { get; set; } = v => { return default(TResult); };

        public Func<ConsoleKey, bool> ValidatationFn { get; set; } = v => { return true; };

        public QuestionInputKey<TResult> ConvertToString(Func<TResult, string> fn)
        {
            ConvertToStringFn = fn;
            return this;
        }

        public QuestionInputKey<TResult> Parse(Func<ConsoleKey, TResult> fn)
        {
            ParseFn = fn;
            return this;
        }

        public QuestionInputKey<TResult> Validation(Func<ConsoleKey, bool> fn)
        {
            ValidatationFn = fn;
            return this;
        }

        public QuestionInputKey<TResult> WithConfirmation()
        {
            HasConfirmation = true;
            return this;
        }

        public QuestionInputKey<TResult> WithDefaultValue(TResult defaultValue)
        {
            DefaultValue = defaultValue;
            HasDefaultValue = true;
            return this;
        }

        internal override TResult Prompt()
        {
            bool tryAgain = true;
            TResult answer = DefaultValue;

            while (tryAgain)
            {
                DisplayQuestion();

                bool isCanceled = false;
                var key = ConsoleHelper.ReadKey(out isCanceled);
                if (isCanceled)
                {
                    IsCanceled = isCanceled;
                    return default(TResult);
                }

                if (key == ConsoleKey.Enter && HasDefaultValue)
                {
                    answer = DefaultValue;
                    tryAgain = Confirm(ConvertToStringFn(answer));
                }
                else if (ValidatationFn(key))
                {
                    answer = ParseFn(key);
                    tryAgain = Confirm(ConvertToStringFn(answer));
                }
            }

            Console.WriteLine();
            return answer;
        }
    }
}