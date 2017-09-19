using ConsoleWizard.Events;
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

        public override IEvent Prompt()
        {
            bool tryAgain = true;
            T answer = DefaultValue;

            while (tryAgain)
            {
                DisplayQuestion();
                var value = Console.ReadKey().Key;
                
                if(value == ConsoleKey.UpArrow)
                {
                    return new SpecialKeyReturnedEvent(value);
                }

                if ((value == ConsoleKey.Enter && HasDefaultValue) || ValidatationFn(value))
                {
                    answer = ParseFn(value);
                    tryAgain = Confirm(answer);
                }
            }
            Answer = answer;
            return new AnswerReturnedEvent<T>(answer);
        }
    }
}