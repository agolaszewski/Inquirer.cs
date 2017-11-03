using System;

namespace ConsoleWizard
{
    public class QuestionPassword<TResult> : QuestionSingleChoiceBase<TResult>
    {
        internal QuestionPassword(string question) : base(question)
        {
        }

        public Func<string, TResult> ParseFn { get; set; } = v => { return default(TResult); };

        public Func<string, bool> ValidatationFn { get; set; } = v => { return true; };

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

        public override TResult Prompt()
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
    }
}