using System;

namespace InquirerCS
{
    public class QuestionRawList<TResult> : QuestionListBase<string, TResult>
    {
        internal QuestionRawList(string question) : base(question)
        {
        }

        protected QuestionRawList(QuestionRawList<TResult> questionRawList) : base(questionRawList)
        {
        }

        public override QuestionListBase<string, TResult> Page(int pageSize)
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

                var value = ReadFn().ToN<int>();

                if (!value.HasValue && HasDefaultValue)
                {
                    return DefaultValue;
                }

                if (!value.HasValue)
                {
                    tryAgain = true;
                    ConsoleHelper.WriteError($"Cannot parse {value} to {typeof(int)}");
                }
                else
                if (Validate(value.Value))
                {
                    answer = ParseFn(value.Value);
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
