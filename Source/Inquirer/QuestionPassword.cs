using System;
using System.Collections.Generic;

namespace InquirerCS
{
    public class QuestionPassword<TResult> : QuestionSingleChoiceBase<TResult>
    {
        internal QuestionPassword(string question) : base(question)
        {
        }

        internal Func<string, TResult> ParseFn { get; set; } = answer => { return default(TResult); };

        internal List<Tuple<Func<string, bool>, Func<string, string>>> ValidatorsString { get; set; } = new List<Tuple<Func<string, bool>, Func<string, string>>>();

        internal List<Tuple<Func<TResult, bool>, Func<TResult, string>>> ValidatorsTResults { get; set; } = new List<Tuple<Func<TResult, bool>, Func<TResult, string>>>();

        public QuestionPassword<TResult> ConvertToString(Func<TResult, string> fn)
        {
            ConvertToStringFn = fn;
            return this;
        }

        public QuestionPassword<TResult> Parse(Func<string, TResult> fn)
        {
            ParseFn = fn;
            return this;
        }

        public QuestionPassword<TResult> WithConfirmation()
        {
            HasConfirmation = true;
            return this;
        }

        public QuestionPassword<TResult> WithDefaultValue(TResult defaultValue)
        {
            DefaultValue = defaultValue;
            HasDefaultValue = true;
            return this;
        }

        public QuestionPassword<TResult> WithValidation(Func<string, bool> fn, Func<string, string> errorMessageFn)
        {
            ValidatorsString.Add(new Tuple<Func<string, bool>, Func<string, string>>(fn, errorMessageFn));
            return this;
        }

        public QuestionPassword<TResult> WithValidation(Func<string, bool> fn, string errorMessage)
        {
            ValidatorsString.Add(new Tuple<Func<string, bool>, Func<string, string>>(fn, answers => { return errorMessage; }));
            return this;
        }

        public QuestionPassword<TResult> WithValidation(Func<TResult, bool> fn, Func<TResult, string> errorMessageFn)
        {
            ValidatorsTResults.Add(new Tuple<Func<TResult, bool>, Func<TResult, string>>(fn, errorMessageFn));
            return this;
        }

        public QuestionPassword<TResult> WithValidation(Func<TResult, bool> fn, string errorMessage)
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
                string value = string.Empty;

                ConsoleKey key;
                do
                {
                    bool isCanceled = false;
                    key = ConsoleHelper.ReadKey(out isCanceled);
                    if (isCanceled)
                    {
                        IsCanceled = isCanceled;
                        return default(TResult);
                    }

                    switch (key)
                    {
                        case (ConsoleKey.Enter):
                            {
                                break;
                            }

                        default:
                            {
                                ConsoleHelper.PositionWrite("*", Console.CursorLeft - 1, Console.CursorTop);
                                value += (char)key;
                                break;
                            }
                    }
                }
                while (key != ConsoleKey.Enter);

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

        protected override bool Confirm(string result)
        {
            if (HasConfirmation)
            {
                Console.Clear();
                ConsoleHelper.Write("Type again : ");

                ConsoleKey key;
                string repeated = string.Empty;
                do
                {
                    bool isCanceled = false;
                    key = ConsoleHelper.ReadKey(out isCanceled);
                    if (isCanceled)
                    {
                        return true;
                    }

                    switch (key)
                    {
                        case (ConsoleKey.Enter):
                            {
                                break;
                            }

                        default:
                            {
                                ConsoleHelper.PositionWrite("*", Console.CursorLeft - 1, Console.CursorTop);
                                repeated += (char)key;
                                break;
                            }
                    }
                }
                while (key != ConsoleKey.Enter);

                if (repeated != result)
                {
                    ConsoleHelper.WriteError("Strings doesn't match");
                    Console.ReadKey();
                    return true;
                }
            }

            return false;
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
