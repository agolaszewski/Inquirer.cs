using System;

namespace InquirerCS
{
    public class QuestionRawList<TResult> : QuestionListBase<TResult>
    {
        internal QuestionRawList(string question) : base(question)
        {
        }

        protected QuestionRawList(QuestionRawList<TResult> questionRawList) : base(questionRawList)
        {
        }

        public override QuestionListBase<TResult> Page(int pageSize)
        {
            return new QuestionPagedRawList<TResult>(this, pageSize);
        }

        public override TResult Prompt()
        {
            bool tryAgain = true;
            TResult answer = DefaultValue;

            while (tryAgain)
            {
                DisplayQuestion();

                Console.WriteLine();
                Console.WriteLine();

                for (int i = 0; i < Choices.Count; i++)
                {
                    ConsoleHelper.WriteLine(DisplayChoice(i));
                }

                Console.WriteLine();
                ConsoleHelper.Write("Answer: ");

                ConsoleKey? isCanceled = null;
                var value = ConsoleHelper.Read(out isCanceled, ConsoleKey.Escape);
                if (isCanceled.HasValue)
                {
                    IsCanceled = true;
                    return default(TResult);
                }

                if (string.IsNullOrWhiteSpace(value) && HasDefaultValue)
                {
                    return DefaultValue;
                }

                var parsedValue = value.ToN<int>();
                if (!parsedValue.HasValue)
                {
                    tryAgain = true;
                    ConsoleHelper.WriteError($"Cannot parse {value} to {typeof(int)}");
                }
                else
                if (Validate(parsedValue.Value))
                {
                    answer = ParseFn(parsedValue.Value);
                    tryAgain = Confirm(ConvertToStringFn(answer));
                }
            }

            Console.WriteLine();
            return answer;
        }

        protected virtual string DisplayChoice(int index)
        {
            return $"[{index + 1}] {ConvertToStringFn(Choices[index])}";
        }
    }
}