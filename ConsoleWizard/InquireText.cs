using System;

namespace ConsoleWizard
{
    public class InquireText<T> : InquireBase<T>
    {
        public System.Func<string, bool> ValidatationFn { get; internal set; }
        public Func<string, T> ParseFn { get; internal set; }

        public InquireText(string question) : base(question)
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

                if ((string.IsNullOrWhiteSpace(value) && HasDefaultValue) || ValidatationFn(value))
                {
                    if (string.IsNullOrWhiteSpace(value))
                    {
                        answer = DefaultValue;
                    }
                    else
                    {
                        answer = ParseFn(value);
                    }
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