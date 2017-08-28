using System;

namespace BetterConsole
{
    public class InquireText<T> : InquireBase<T>
    {
        public System.Func<string, bool> ValidatationFn { get; internal set; }
        public Func<string, T> ParseFn { get; internal set; }

        public override T Prompt()
        {
            bool tryAgain = true;
            T result = DefaultValue;

            while (tryAgain)
            {
                DisplayQuestion();
                var value = Console.ReadLine();

                if ((string.IsNullOrWhiteSpace(value) && HasDefaultValue) || ValidatationFn(value))
                {
                    if (string.IsNullOrWhiteSpace(value))
                    {
                        result = DefaultValue;
                    }
                    else
                    {
                        result = ParseFn(value);
                    }
                    tryAgain = Confirm(result);
                }
            }

            Console.WriteLine();
            this.NavigateFn(result);
            return result;
        }
    }
}