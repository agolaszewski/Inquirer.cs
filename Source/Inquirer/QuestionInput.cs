using System;

namespace InquirerCS
{
    public class QuestionInput<TResult> : QuestionSingleChoiceBase<TResult>
    {
        internal QuestionInput(string message) : base(message)
        {
        }

        internal Func<string, TResult> ParseFn { get; set; } = answer => { return default(TResult); };

        internal Func<string, bool> ValidatationFn { get; set; } = answer => { return true; };

        internal string ErrorMessage { get; set; }

        public QuestionInput<TResult> WithValidatation(Func<string, bool> fn, string errorMessage)
        {
            ValidatationFn = fn;
            ErrorMessage = errorMessage;
            return this;
        }

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
                else
                {
                    Console.ReadKey();
                }
            }

            return answer;
        }
    }
}