using System;
using ConsoleWizard.Components;

namespace ConsoleWizard
{
    public class QuestionInput<T> : QuestionSingleChoiceBase<T>, IConvertToString<T>, IConvertToResult<string, T>, IValidation<string>
    {
        internal QuestionInput(string message) : base(message)
        {
        }

        public Func<string, T> ParseFn { get; set; } = v => { return default(T); };

        public Func<string, bool> ValidatationFn { get; set; } = v => { return true; };

        internal override T Prompt()
        {
            bool tryAgain = true;
            T answer = DefaultValue;

            while (tryAgain)
            {
                DisplayQuestion();

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
                    tryAgain = Confirm(ConvertToStringFn(answer));
                }
                else if (ValidatationFn(value))
                {
                    answer = ParseFn(value);
                    tryAgain = Confirm(ConvertToStringFn(answer));
                }
            }

            return answer;
        }
    }
}