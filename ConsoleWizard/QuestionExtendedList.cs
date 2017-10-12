using System;
using System.Collections.Generic;

namespace ConsoleWizard
{
    public class QuestionExtendedList<TDictionary, T> : QuestionDictionaryListBase<TDictionary, T> where TDictionary : Dictionary<ConsoleKey, T>, new()
    {
        internal QuestionExtendedList(string question) : base(question)
        {
        }

        internal Func<ConsoleKey, T, string> ChoicesDisplayFn { get; set; }

        internal Func<ConsoleKey, T> ParseFn { get; set; } = v => { return default(T); };

        internal Func<ConsoleKey, bool> ValidatationFn { get; set; } = v => { return true; };

        internal override T Prompt()
        {
            bool tryAgain = true;
            T answer = DefaultValue;

            while (tryAgain)
            {
                DisplayQuestion(ToStringFn(answer));

                Console.WriteLine();
                Console.WriteLine();

                foreach (var item in Choices)
                {
                    ConsoleHelper.WriteLine(ChoicesDisplayFn(item.Key, item.Value));
                }

                Console.WriteLine();
                ConsoleHelper.Write("Answer: ");

                bool isCanceled = false;
                var key = ConsoleHelper.ReadKey(out isCanceled);
                if (isCanceled)
                {
                    IsCanceled = isCanceled;
                    return default(T);
                }

                if (key == ConsoleKey.Enter && HasDefaultValue)
                {
                    tryAgain = Confirm(ToStringFn(answer));
                }
                else if (ValidatationFn(key))
                {
                    answer = ParseFn(key);
                    tryAgain = Confirm(ToStringFn(answer));
                }
            }

            Console.WriteLine();
            return answer;
        }
    }
}