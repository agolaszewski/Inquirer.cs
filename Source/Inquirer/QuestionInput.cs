namespace InquirerCS
{
    public class QuestionInput<TResult> : QuestionSingleChoiceBase<string, TResult>
    {
        internal QuestionInput(string message) : base(message)
        {
        }

        public override TResult Prompt()
        {
            bool tryAgain = true;
            TResult answer = DefaultValue;

            while (tryAgain)
            {
                DisplayQuestion();

                var value = ReadFn();

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