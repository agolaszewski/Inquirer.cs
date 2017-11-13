using System;

namespace InquirerCS
{
    public class QuestionInputKey<TResult> : QuestionSingleChoiceBase<ConsoleKey, TResult>
    {
        internal QuestionInputKey(string question) : base(question)
        {
        }

        internal override TResult Prompt()
        {
            bool tryAgain = true;
            TResult answer = DefaultValue;

            while (tryAgain)
            {
                DisplayQuestion();

                bool isCanceled = false;
                var key = ConsoleHelper.ReadKey(out isCanceled);
                if (isCanceled)
                {
                    IsCanceled = isCanceled;
                    return default(TResult);
                }

                if (key == ConsoleKey.Enter && HasDefaultValue)
                {
                    answer = DefaultValue;
                    tryAgain = Confirm(ConvertToStringFn(answer));
                }
                else if (Validate(key))
                {
                    answer = ParseFn(key);
                    tryAgain = Confirm(ConvertToStringFn(answer));
                }
            }

            Console.WriteLine();
            return answer;
        }
    }
}
