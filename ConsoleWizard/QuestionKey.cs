using System;
using ConsoleWizard.Components;

namespace ConsoleWizard
{
    public class QuestionInputKey<T> : QuestionBase<T>, IConvertToString<T>, IConvertToResult<ConsoleKey, T>, IValidation<ConsoleKey>
    {
        private DisplayQuestionSingleChoiceComponent<QuestionInputKey<T>, T> _displayQuestionComponent;

        internal QuestionInputKey(string question) : base(question)
        {
            _displayQuestionComponent = new DisplayQuestionSingleChoiceComponent<QuestionInputKey<T>, T>(this);
        }

        public Func<T, string> ConvertToStringFn { get; set; } = value => { return value.ToString(); };

        public Func<ConsoleKey, T> ParseFn { get; set; } = v => { return default(T); };

        public Func<ConsoleKey, bool> ValidatationFn { get; set; } = v => { return true; };

        internal override T Prompt()
        {
            bool tryAgain = true;
            T answer = DefaultValue;

            while (tryAgain)
            {
                _displayQuestionComponent.DisplayQuestion();

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
                    tryAgain = Confirm(ConvertToStringFn(answer));
                }
                else if (ValidatationFn(key))
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