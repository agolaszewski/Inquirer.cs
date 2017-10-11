using System;

namespace ConsoleWizard
{
    public class QuestionPassword<T> : QuestionBase<T>
    {
        internal QuestionPassword(string message) : base(message)
        {
        }

        internal Func<string, T> ParseFn { get; set; } = v => { return default(T); };

        internal Func<string, bool> ValidatationFn { get; set; } = v => { return true; };

        internal override T Prompt()
        {
            bool tryAgain = true;
            T answer = DefaultValue;

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
                        return default(T);
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
                    tryAgain = Confirm(answer);
                }
                else if (ValidatationFn(value))
                {
                    answer = ParseFn(value);
                    tryAgain = Confirm(answer);
                }
            }

            return answer;
        }
    }
}