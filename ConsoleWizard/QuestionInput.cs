using System;

namespace ConsoleWizard
{
    public class QuestionInput<TResult> : QuestionSingleChoiceBase<TResult>
    {
        internal QuestionInput(string message) : base(message)
        {
        }

        public Func<string, TResult> ParseFn { get; set; } = v => { return default(TResult); };

        public Func<string, bool> ValidatationFn { get; set; } = v => { return true; };

        public QuestionInput<TResult> ConvertToString(Func<TResult, string> fn)
        {
            ConvertToStringFn = fn;
            return this;
        }

        public QuestionInput<TResult> Parse(Func<string, TResult> fn)
        {
            ParseFn = fn;
            return this;
        }

        public QuestionInput<TResult> Validation(Func<string, bool> fn)
        {
            ValidatationFn = fn;
            return this;
        }

        public QuestionInput<TResult> WithConfirmation()
        {
            HasConfirmation = true;
            return this;
        }

        public QuestionInput<TResult> WithDefaultValue(TResult defaultValue)
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
                var value = ConsoleHelper.Read(out isCanceled);
                if (isCanceled)
                {
                    IsCanceled = isCanceled;
                    return default(TResult);
                }

                if (string.IsNullOrWhiteSpace(value) && HasDefaultValue)
                {
                    answer = DefaultValue;
                    tryAgain = Confirm(ConvertToStringFn(answer));
                }
                else if (ValidatationFn(value))
                {
                    answer = ParseFn(value);
                    tryAgain = Confirm(ConvertToStringFn(answer));
                }
            }

            return answer;
        }
    }
}