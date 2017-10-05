using System;
using System.Collections.Generic;

namespace ConsoleWizard
{
    public class QuestionExtendedList<TDictionary, T> : QuestionDictionaryListBase<TDictionary, T> where TDictionary : Dictionary<ConsoleKey, T>, new()
    {
        public Func<ConsoleKey, bool> ValidatationFn { get; set; } = v => { return true; };
        public Func<ConsoleKey, T> ParseFn { get; set; } = v => { return default(T); };

        public Func<ConsoleKey, T, string> DisplayQuestionAnswersFn { get; set; }

        public QuestionExtendedList(string question) : base(question)
        {
        }

        public override T Prompt()
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
            Answer = answer;
            Console.WriteLine();
            return answer;
        }
    }
}