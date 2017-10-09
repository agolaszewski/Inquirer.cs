using System;

namespace ConsoleWizard
{
    public class QuestionInputKey<T> : QuestionBase<T>
    {
        internal QuestionInputKey(string question) : base(question)
        {
        }

        internal Func<ConsoleKey, T> ParseFn { get; set; } = v => { return default(T); };

        internal Func<ConsoleKey, bool> ValidatationFn { get; set; } = v => { return true; };

        internal override T Prompt()
        {
            bool tryAgain = true;
            T answer = DefaultValue;

            while (tryAgain)
            {
                DisplayQuestion();
                var value = Console.ReadKey().Key;

                if (value == ConsoleKey.Enter && HasDefaultValue)
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

            Console.WriteLine();
            return answer;
        }
    }
}