using System;

namespace ConsoleWizard
{
    public class QuestionRawList<T> : QuestionListBase<T>
    {
        internal QuestionRawList(string question) : base(question)
        {
        }

        internal Func<int, T, string> ChoicesDisplayFn { get; set; }

        internal Func<int, T> ParseFn { get; set; } = v => { return default(T); };

        internal Func<int, bool> ValidatationFn { get; set; } = v => { return true; };

        internal override T Prompt()
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
                    ConsoleHelper.WriteLine(ChoicesDisplayFn(i + 1, Choices[i]));
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

            Console.WriteLine();
            return answer;
        }
    }
}