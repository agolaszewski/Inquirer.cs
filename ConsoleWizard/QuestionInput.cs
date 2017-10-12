using System;

namespace ConsoleWizard
{
    public class QuestionInput<T> : QuestionBase<T>
    {
        internal QuestionInput(string message) : base(message)
        {
        }

        public Func<T, string> ToStringFn { get; set; } = value => { return value.ToString(); };

        internal Func<string, T> ParseFn { get; set; } = v => { return default(T); };

        internal Func<string, bool> ValidatationFn { get; set; } = v => { return true; };

        internal override T Prompt()
        {
            bool tryAgain = true;
            T answer = DefaultValue;

            while (tryAgain)
            {
                DisplayQuestion(ToStringFn(answer));

                bool isCanceled = false;
                var value = ConsoleHelper.Read(out isCanceled);
                if (isCanceled)
                {
                    IsCanceled = isCanceled;
                    return default(T);
                }

                if (string.IsNullOrWhiteSpace(value) && HasDefaultValue)
                {
                    answer = DefaultValue;
                    tryAgain = Confirm(ToStringFn(answer));
                }
                else if (ValidatationFn(value))
                {
                    answer = ParseFn(value);
                    tryAgain = Confirm(ToStringFn(answer));
                }
            }

            return answer;
        }
    }
}
