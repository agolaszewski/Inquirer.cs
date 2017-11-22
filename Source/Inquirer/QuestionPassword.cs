using System;

namespace InquirerCS
{
    public class QuestionPassword<TResult> : QuestionSingleChoiceBase<ConsoleKey, string, TResult>
    {
        internal QuestionPassword(string question) : base(question)
        {
        }

        public override TResult Prompt()
        {
            bool tryAgain = true;
            TResult answer = DefaultValue;

            while (tryAgain)
            {
                DisplayQuestion();
                string value = string.Empty;

                ConsoleKey key;
                do
                {
                    key = ReadFn();

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
                    value = ConvertToStringFn(DefaultValue);
                }

                if (Validate(value))
                {
                    tryAgain = Confirm(ConvertToStringFn(answer));
                    answer = ParseFn(value);
                }
            }

            return answer;
        }

        protected override bool Confirm(string result)
        {
            if (HasConfirmation)
            {
                Console.Clear();
                ConsoleHelper.Write("Type again : ");

                ConsoleKey key;
                string repeated = string.Empty;
                do
                {
                    bool isCanceled = false;
                    key = ConsoleHelper.ReadKey(out isCanceled);
                    if (isCanceled)
                    {
                        return true;
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
                                repeated += (char)key;
                                break;
                            }
                    }
                }
                while (key != ConsoleKey.Enter);

                if (repeated != result)
                {
                    ConsoleHelper.WriteError("Strings doesn't match");
                    return true;
                }
            }

            return false;
        }
    }
}