using System;

namespace InquirerCS
{
    public class QuestionRawList<TResult> : QuestionListBase<TResult>
    {
        internal QuestionRawList(string question) : base(question)
        {
        }

        internal override TResult Prompt()
        {
            bool tryAgain = true;
            TResult answer = DefaultValue;

            while (tryAgain)
            {
                DisplayQuestion();

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
                    return default(TResult);
                }

                if (value.HasValue == false && HasDefaultValue)
                {
                    tryAgain = Confirm(ConvertToStringFn(answer));
                }
                else if (value.HasValue && Validate(value.Value))
                {
                    answer = ParseFn(value.Value);
                    tryAgain = Confirm(ConvertToStringFn(answer));
                }
            }

            Console.WriteLine();
            return answer;
        }

        protected string DisplayChoice(int index, TResult choice)
        {
            return $"[{index}] {ConvertToStringFn(choice)}";
        }
    }
}
