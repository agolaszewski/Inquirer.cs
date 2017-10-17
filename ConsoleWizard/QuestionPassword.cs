using System;
using ConsoleWizard.Components;

namespace ConsoleWizard
{
    public class QuestionPassword<T> : QuestionBase<T>, IConvertToString<T>, IConvertToResult<string, T>, IValidation<string>
    {
        private DisplayQuestionSingleChoiceComponent<QuestionPassword<T>, T> _displayQuestionComponent;

        internal QuestionPassword(string question) : base(question)
        {
            _displayQuestionComponent = new DisplayQuestionSingleChoiceComponent<QuestionPassword<T>, T>(this);
        }

        public Func<T, string> ConvertToStringFn { get; set; } = value => { return value.ToString(); };

        public Func<string, T> ParseFn { get; set; } = v => { return default(T); };

        public Func<string, bool> ValidatationFn { get; set; } = v => { return true; };

        internal override T Prompt()
        {
            bool tryAgain = true;
            T answer = DefaultValue;

            while (tryAgain)
            {
                _displayQuestionComponent.DisplayQuestion();
                string value = string.Empty;

                ConsoleKey key;
                do
                {
                    bool isCanceled = false;
                    key = ConsoleHelper.ReadKey(out isCanceled);
                    if (isCanceled)
                    {
                        IsCanceled = isCanceled;
                        return default(T);
                    }

                    switch (key)
                    {
                        case (ConsoleKey.Enter):
                            {
                                break;
                            }

                        default:
                            {
                                ConsoleHelper.PositionWrite("*", Console.CursorLeft - 1, Console.CursorTop);
                                value += (char)key;
                                break;
                            }
                    }
                }
                while (key != ConsoleKey.Enter);

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