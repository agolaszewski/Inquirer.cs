using System;

namespace ConsoleWizard
{
    public class QuestionKey<T> : QuestionBase<T>
    {
        public Func<ConsoleKey, bool> ValidatationFn { get; set; } = v => { return true; };
        public Func<ConsoleKey, T> ParseFn { get; set; } = v => { return default(T); };

        public QuestionKey(string question) : base(question)
        {
        }

        public override T Prompt()
        {
            bool tryAgain = true;
            T answer = DefaultValue;

            while (tryAgain)
            {
                DisplayQuestion();
                var value = Console.ReadKey().Key;

                if ((value == ConsoleKey.Enter && HasDefaultValue) || ValidatationFn(value))
                {
                    answer = ParseFn(value);
                    tryAgain = Confirm(answer);
                }
            }
            Answer = answer;
            Console.WriteLine();
            return answer;
        }
    }
}