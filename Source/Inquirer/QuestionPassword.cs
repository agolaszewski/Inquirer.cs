using System;

namespace InquirerCS
{
    public class QuestionPassword<TResult> : QuestionSingleChoiceBase<TResult>
    {
        internal QuestionPassword(string question) : base(question)
        {
        }

        internal string ErrorMessage { get; set; }

        internal Func<string, TResult> ParseFn { get; set; } = answer => { return default(TResult); };

        internal Func<string, bool> ValidatationFn { get; set; } = answer => { return true; };

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

        public QuestionPassword<TResult> Validation(Func<string, bool> fn)
        {
            ValidatationFn = fn;
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

        public QuestionPassword<TResult> WithValidatation(Func<string, bool> fn, string errorMessage)
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
                else if (ValidatationFn(value))
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
    }
}
