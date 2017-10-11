using System;

namespace ConsoleWizard
{
    public class QuestionInputKey<T> : QuestionBase<T>
    {
        internal QuestionInputKey(string question) : base(question)
        {
        }

        internal Func<ConsoleKey, T> ParseFn { get; set; } = v => { return default(T); };

        internal Func<ConsoleKey, bool> ValidatationFn { get; set; } = v => { return true; };

        internal override T Prompt()
        {
            bool tryAgain = true;
            T answer = DefaultValue;

            while (tryAgain)
            {
                DisplayQuestion();

                bool isCanceled = false;
                var key = ConsoleHelper.ReadKey(out isCanceled);
                if (isCanceled)
                {
                    IsCanceled = isCanceled;
                    return default(T);
                }

                if (key == ConsoleKey.Enter && HasDefaultValue)
                {
                    answer = DefaultValue;
                    tryAgain = Confirm(answer);
                }
                else if (ValidatationFn(key))
                {
                    answer = ParseFn(key);
                    tryAgain = Confirm(answer);
                }
            }

            Console.WriteLine();
            return answer;
        }
    }
}