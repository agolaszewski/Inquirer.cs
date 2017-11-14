namespace InquirerCS
{
    public class QuestionInput<TResult> : QuestionSingleChoiceBase<string, TResult>
    {
        internal QuestionInput(string message) : base(message)
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
                var value = ConsoleHelper.Read(out isCanceled);
                if (isCanceled)
                {
                    IsCanceled = isCanceled;
                    return default(TResult);
                }

                if (string.IsNullOrWhiteSpace(value) && HasDefaultValue)
                {
                    answer = DefaultValue;
                    tryAgain = Confirm(ConvertToStringFn(answer));
                }
                else if (Validate(value))
                {
                    answer = ParseFn(value);
                    tryAgain = Confirm(ConvertToStringFn(answer));
                }
            }

            return answer;
        }
    }
}