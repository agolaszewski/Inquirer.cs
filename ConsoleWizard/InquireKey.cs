using System;

namespace ConsoleWizard
{
    public class InquireKey<T> : InquireBase<T>
    {
        public Func<ConsoleKey, bool> ValidatationFn { get; internal set; }
        public Func<ConsoleKey, T> ParseFn { get; internal set; }

        public InquireKey(string question) : base(question)
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
            ResultFn(answer);
            Answer = answer;
            Console.WriteLine();
            return answer;
        }
    }
}