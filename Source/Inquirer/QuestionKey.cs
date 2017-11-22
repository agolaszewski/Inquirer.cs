using System;

namespace InquirerCS
{
    public class QuestionInputKey<TResult> : QuestionSingleChoiceBase<ConsoleKey, ConsoleKey, TResult>
    {
        internal QuestionInputKey(string question) : base(question)
        {
        }

        public override TResult Prompt()
        {
            bool tryAgain = true;
            TResult answer = DefaultValue;

            while (tryAgain)
            {
                DisplayQuestion();

                var key = ReadFn();

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
