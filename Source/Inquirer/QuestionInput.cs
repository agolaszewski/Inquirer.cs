using System;
using System.Collections.Generic;

namespace InquirerCS
{
    public class QuestionInput<TResult> : QuestionSingleChoiceBase<TResult>
    {
        internal QuestionInput(string message) : base(message)
        {
        }

        internal Func<string, TResult> ParseFn { get; set; } = answer => { return default(TResult); };

        internal List<Tuple<Func<string, bool>, Func<string, string>>> ValidatorsString { get; set; } = new List<Tuple<Func<string, bool>, Func<string, string>>>();

        internal List<Tuple<Func<TResult, bool>, Func<TResult, string>>> ValidatorsTResults { get; set; } = new List<Tuple<Func<TResult, bool>, Func<TResult, string>>>();

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

        public QuestionInput<TResult> WithValidation(Func<string, bool> fn, Func<string, string> errorMessageFn)
        {
            ValidatorsString.Add(new Tuple<Func<string, bool>, Func<string, string>>(fn, errorMessageFn));
            return this;
        }

        public QuestionInput<TResult> WithValidation(Func<string, bool> fn, string errorMessage)
        {
            ValidatorsString.Add(new Tuple<Func<string, bool>, Func<string, string>>(fn, answers => { return errorMessage; }));
            return this;
        }

        public QuestionInput<TResult> WithValidation(Func<TResult, bool> fn, Func<TResult, string> errorMessageFn)
        {
            ValidatorsTResults.Add(new Tuple<Func<TResult, bool>, Func<TResult, string>>(fn, errorMessageFn));
            return this;
        }

        public QuestionInput<TResult> WithValidation(Func<TResult, bool> fn, string errorMessage)
        {
            ValidatorsTResults.Add(new Tuple<Func<TResult, bool>, Func<TResult, string>>(fn, answers => { return errorMessage; }));
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
                else if (Validate(value))
                {
                    answer = ParseFn(value);
                    tryAgain = Confirm(ConvertToStringFn(answer));
                }
            }

            return answer;
        }

        private bool Validate(string value)
        {
            foreach (var validator in ValidatorsString)
            {
                if (!validator.Item1(value))
                {
                    ConsoleHelper.WriteError(validator.Item2(value));
                    return false;
                }
            }

            TResult answer = default(TResult);
            try
            {
                answer = ParseFn(value);
            }
            catch
            {
                ConsoleHelper.WriteError($"Cannot parse {value} to {typeof(TResult)}");
                return false;
            }

            foreach (var validator in ValidatorsTResults)
            {
                if (!validator.Item1(answer))
                {
                    ConsoleHelper.WriteError(validator.Item2(answer));
                    return false;
                }
            }

            return true;
        }
    }
}
