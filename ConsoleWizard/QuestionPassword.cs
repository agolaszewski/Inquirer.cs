using System;

namespace ConsoleWizard
{
    public class QuestionPassword<T> : QuestionBase<T>
    {
        public Func<string, bool> ValidatationFn { get; set; } = v => { return true; };
        public Func<string, T> ParseFn { get; set; } = v => { return default(T); };

        public QuestionPassword(string message) : base(message)
        {
        }

        public override T Prompt()
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
                    key = Console.ReadKey().Key;

                    switch (key)
                    {
                        case (ConsoleKey.Enter):
                            {
                                break;
                            }
                        default:
                            {
                                Console.SetCursorPosition(Console.CursorLeft - 1, Console.CursorTop);
                                ConsoleHelper.Write("*");
                                value += (char)key;
                                break;
                            }
                    }
                } while (key != ConsoleKey.Enter);

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