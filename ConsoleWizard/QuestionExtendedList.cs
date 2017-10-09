using System;
using System.Collections.Generic;

namespace ConsoleWizard
{
    public class QuestionExtendedList<TDictionary, T> : QuestionDictionaryListBase<TDictionary, T> where TDictionary : Dictionary<ConsoleKey, T>, new()
    {
        internal QuestionExtendedList(string question) : base(question)
        {
        }

        internal Func<ConsoleKey, T, string> DisplayQuestionAnswersFn { get; set; }

        internal Func<ConsoleKey, T> ParseFn { get; set; } = v => { return default(T); };

        internal Func<ConsoleKey, bool> ValidatationFn { get; set; } = v => { return true; };

        internal override T Prompt()
        {
            bool tryAgain = true;
            T answer = DefaultValue;

            while (tryAgain)
            {
                DisplayQuestion();

                Console.WriteLine();
                Console.WriteLine();

                foreach (var item in Choices)
                {
                    ConsoleHelper.WriteLine(DisplayQuestionAnswersFn(item.Key, item.Value));
                }

                Console.WriteLine();
                ConsoleHelper.Write("Answer: ");
                var value = Console.ReadKey().Key;

                if (value == ConsoleKey.Enter && HasDefaultValue)
                {
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