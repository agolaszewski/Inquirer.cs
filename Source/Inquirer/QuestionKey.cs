using System;
using System.Collections.Generic;

namespace InquirerCS
{
    public class QuestionInputKey<TResult> : QuestionSingleChoiceBase<TResult>
    {
        internal QuestionInputKey(string question) : base(question)
        {
        }

        internal Func<ConsoleKey, TResult> ParseFn { get; set; } = answer => { return default(TResult); };

        internal List<Tuple<Func<ConsoleKey, bool>, Func<ConsoleKey, string>>> ValidatorsKey { get; set; } = new List<Tuple<Func<ConsoleKey, bool>, Func<ConsoleKey, string>>>();

        internal List<Tuple<Func<TResult, bool>, Func<TResult, string>>> ValidatorsTResults { get; set; } = new List<Tuple<Func<TResult, bool>, Func<TResult, string>>>();

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

        public QuestionInputKey<TResult> WithValidatation(Func<ConsoleKey, bool> fn, Func<ConsoleKey, string> errorMessageFn)
        {
            ValidatorsKey.Add(new Tuple<Func<ConsoleKey, bool>, Func<ConsoleKey, string>>(fn, errorMessageFn));
            return this;
        }

        public QuestionInputKey<TResult> WithValidatation(Func<ConsoleKey, bool> fn, string errorMessage)
        {
            ValidatorsKey.Add(new Tuple<Func<ConsoleKey, bool>, Func<ConsoleKey, string>>(fn, answers => { return errorMessage; }));
            return this;
        }

        public QuestionInputKey<TResult> WithValidatation(Func<TResult, bool> fn, Func<TResult, string> errorMessageFn)
        {
            ValidatorsTResults.Add(new Tuple<Func<TResult, bool>, Func<TResult, string>>(fn, errorMessageFn));
            return this;
        }

        public QuestionInputKey<TResult> WithValidatation(Func<TResult, bool> fn, string errorMessage)
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
                else if (Validate(key))
                {
                    answer = ParseFn(key);
                    tryAgain = Confirm(ConvertToStringFn(answer));
                }
            }

            Console.WriteLine();
            return answer;
        }

        private bool Validate(ConsoleKey value)
        {
            foreach (var validator in ValidatorsKey)
            {
                if (validator.Item1(value))
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
                if (validator.Item1(answer))
                {
                    ConsoleHelper.WriteError(validator.Item2(answer));
                    return false;
                }
            }

            return true;
        }
    }
}