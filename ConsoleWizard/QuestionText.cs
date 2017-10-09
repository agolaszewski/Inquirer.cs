using System;

namespace ConsoleWizard
{
    public class QuestionInput<T> : QuestionBase<T>
    {
        internal QuestionInput(string message) : base(message)
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
                var value = Console.ReadLine();

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