using System;
using System.Collections.Generic;

namespace BetterConsole
{
    public class InquireSingleChoice<T> : InquireMultiAnswersBase<T>
    {
        public List<T> Choices { get; internal set; }
        public Func<T, string> ChoiceToStringFn { get; internal set; }
        public Func<string, bool> ValidatationFn { get; internal set; }
        public Func<string, T> ParseFn { get; internal set; }
       
        public override void Prompt()
        {
            bool tryAgain = true;
            T result = DefaultValue;

            while (tryAgain)
            {
                DisplayChoices();
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
            ResultFn(result);
            Console.WriteLine();
        }

        private void DisplayChoices()
        {
            for(int i=0; i < Choices.Count; i++)
            {
                Console.WriteLine($"[{i + 1}] {ChoiceToStringFn(Choices[i])}");
            }
        }
    }
}