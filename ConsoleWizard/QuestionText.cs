using System;

namespace ConsoleWizard
{
    public class QuestionText<T> : QuestionBase<T>
    {
        public Func<string, bool> ValidatationFn { get; set; } = v => { return true; };
        public Func<string, T> ParseFn { get; set; } = v => { return default(T); };

        public QuestionText(string message) : base(message)
        {
        }

        public override T Prompt()
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
                }
                else if (ValidatationFn(value))
                {
                    answer = ParseFn(value);
                }
                tryAgain = Confirm(answer);
            }

            return answer;
        }
    }
}