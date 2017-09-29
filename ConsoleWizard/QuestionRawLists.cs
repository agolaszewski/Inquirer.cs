using System;
using System.Collections.Generic;

namespace ConsoleWizard
{
    public class QuestionRawList<T> : QuestionBase<T>
    {
        public Func<int, bool> ValidatationFn { get; set; } = v => { return true; };
        public Func<int, T> ParseFn { get; set; } = v => { return default(T); };

        public Func<int, T, string> DisplayQuestionAnswersFn { get; set; }

        public List<T> Choices { get; internal set; }

        public QuestionRawList(string question) : base(question)
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

                for (int i = 0; i < Choices.Count; i++)
                {
                    ConsoleHelper.WriteLine(DisplayQuestionAnswersFn(i + 1, Choices[i]));
                }

                Console.WriteLine();
                ConsoleHelper.Write("Answer: ");
                var value = Console.ReadLine().ToN<int>();

                if (value.HasValue == false && HasDefaultValue)
                {
                    tryAgain = Confirm(answer);
                }
                else if (value.HasValue && ValidatationFn(value.Value))
                {
                    answer = ParseFn(value.Value);
                    tryAgain = Confirm(answer);
                }
            }
            Answer = answer;
            Console.WriteLine();
            return answer;
        }
    }
}