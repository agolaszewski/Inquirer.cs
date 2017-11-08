using System;

namespace InquirerCS
{
    internal class QuestionPagedRawList<T> : QuestionRawList<T> 
    {
        private int _skipChoices = 0;

        public QuestionPagedRawList(QuestionRawList<T> question) : base(question.Message)
        {
            ValidatationFn = question.ValidatationFn;
            ParseFn = question.ParseFn;
            Choices = question.Choices;
        }

        public int PageSize { get; internal set; } = 0;

        public override T Prompt()
        {
            bool tryAgain = true;
            T answer = DefaultValue;

            DisplayQuestion();

            while (tryAgain)
            {
                Console.WriteLine();
                Console.WriteLine();

                DisplayChoices();

                Console.WriteLine();
                ConsoleHelper.Write("Answer: ");

                string result = string.Empty;
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
                        case (ConsoleKey.LeftArrow):
                            {
                                if (_skipChoices - PageSize >= 0)
                                {
                                    _skipChoices = _skipChoices - PageSize;
                                    return Prompt();
                                }

                                break;
                            }

                        case (ConsoleKey.RightArrow):
                            {
                                _skipChoices = MathHelper.Clamp(_skipChoices + PageSize, 0, Choices.Count);
                                if (_skipChoices != Choices.Count)
                                {
                                    return Prompt();
                                }

                                break;
                            }

                        default:
                            {
                                result += (char)key;
                                break;
                            }
                    }
                }
                while (key != ConsoleKey.Enter);

                Console.WriteLine();
                ConsoleHelper.Write("Answer: ");

                var value = result.ToN<int>();

                if (value.HasValue == false && HasDefaultValue)
                {
                    tryAgain = Confirm(ConvertToStringFn(answer));
                }
                else if (value.HasValue && ValidatationFn(value.Value))
                {
                    answer = ParseFn(value.Value);
                    tryAgain = Confirm(ConvertToStringFn(answer));
                }
            }

            Console.WriteLine();
            return answer;
        }

        private void DisplayChoices()
        {
            if (_skipChoices != 0)
            {
                ConsoleHelper.WriteLine("[←] Previous Page");
            }

            int max = MathHelper.Clamp(_skipChoices + PageSize, 0, Choices.Count);
            for (int i = _skipChoices; i < max; i++)
            {
                ConsoleHelper.WriteLine(DisplayChoice(i + 1, Choices[i]));
            }

            if (max != Choices.Count)
            {
                ConsoleHelper.WriteLine("[→] Next Page");
            }
        }
    }
}