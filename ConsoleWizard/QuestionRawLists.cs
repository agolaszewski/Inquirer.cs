using System;
using ConsoleWizard.Components;

namespace ConsoleWizard
{
    public class QuestionRawList<T> : QuestionListBase<T>, IConvertToResult<int, T>, IValidation<int>
    {
        internal QuestionRawList(string question) : base(question)
        {
        }

        public Func<int, T> ParseFn { get; set; } = v => { return default(T); };

        public Func<int, bool> ValidatationFn { get; set; } = v => { return true; };

        internal override T Prompt()
        {
            bool tryAgain = true;
            T answer = DefaultValue;

            while (tryAgain)
            {
                DisplayQuestion(this);

                Console.WriteLine();
                Console.WriteLine();

                for (int i = 0; i < Choices.Count; i++)
                {
                    ConsoleHelper.WriteLine(DisplayChoice(i + 1, Choices[i]));
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

        protected string DisplayChoice(int index, T choice)
        {
            return $"[{index}] {choice}";
        }
    }
}
