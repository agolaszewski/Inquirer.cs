using System;

namespace BetterConsole
{
    public class InquireKey<T> : InquireBase<T>
    {
        public Func<ConsoleKey, bool> ValidatationFn { get; internal set; }
        public Func<ConsoleKey, T> ParseFn { get; internal set; }

        public override T Prompt()
        {
            bool tryAgain = true;
            T result = DefaultValue;

            while (tryAgain)
            {
                DisplayQuestion();
                var value = Console.ReadKey().Key;

                if ((value == ConsoleKey.Enter && HasDefaultValue) || ValidatationFn(value))
                {
                    result = ParseFn(value);
                    tryAgain = Confirm(result);
                }
            }

            Console.WriteLine();
            this.NavigateFn(result);
            return result;
        }
    }
}