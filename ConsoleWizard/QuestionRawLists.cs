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
                 DisplayQuestion(ToStringFn(answer));

                Console.WriteLine();
                Console.WriteLine();

                for (int i = 0; i < Choices.Count; i++)
                {
                    ConsoleHelper.WriteLine(ChoicesDisplayFn(i + 1, Choices[i]));
                }

                Console.WriteLine();
                ConsoleHelper.Write("Answer: ");

                bool isCanceled = false;
                var value = ConsoleHelper.Read(out isCanceled).ToN<int>();
                if (isCanceled)
                {
                    IsCanceled = isCanceled;
                    return default(T);
                }

                if (value.HasValue == false && HasDefaultValue)
                {
                    tryAgain = Confirm(ToStringFn(answer));
                }
                else if (value.HasValue && ValidatationFn(value.Value))
                {
                    answer = ParseFn(value.Value);
                    tryAgain = Confirm(ToStringFn(answer));
                }
            }

            Console.WriteLine();
            return answer;
        }
    }
}