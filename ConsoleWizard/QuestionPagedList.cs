using System;
using System.Collections.Generic;

namespace ConsoleWizard
{
    internal class QuestionPagedList<T> : QuestionBase<T>
    {
        public Func<int, bool> ValidatationFn { get; set; } = v => { return true; };
        public Func<int, T> ParseFn { get; set; } = v => { return default(T); };

        public Func<int, T, string> DisplayQuestionAnswersFn { get; set; }

        public List<T> Choices { get; internal set; }

        public int PageSize { get; internal set; } = 0;

        private int _skipChoices = 0;

        public QuestionPagedList(string question) : base(question)
        {
        }

        public override T Prompt()
        {
            bool tryAgain = true;
            T answer = DefaultValue;

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
                    key = Console.ReadKey().Key;
                    if (key == ConsoleKey.LeftArrow)
                    {
                        _skipChoices = MathHelper.Clamp(_skipChoices - PageSize, 0, Choices.Count);
                        if (_skipChoices - PageSize >= 0)
                        {
                            return Prompt();
                        }
                    }
                    else if (key == ConsoleKey.RightArrow)
                    {
                        _skipChoices = MathHelper.Clamp(_skipChoices + PageSize, 0, Choices.Count);
                        if (_skipChoices != Choices.Count)
                        {
                            return Prompt();
                        }
                    }
                    else if (key != ConsoleKey.Enter)
                    {
                        result += (char)key;
                    }
                } while (key != ConsoleKey.Enter);

                Console.WriteLine();
                ConsoleHelper.Write("Answer: ");
                var value = result.ToN<int>();

                if (value.HasValue == false && HasDefaultValue)
                {
                    tryAgain = Confirm(answer);
                }
                else if (value.HasValue && ValidatationFn(value.Value))
                {
                    answer = ParseFn(value.Value);
                    tryAgain = Confirm(answer);
                }
            }
            Answer = answer;
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
                DisplayQuestionAnswersFn(i + 1, Choices[i]);
            }

            if (PageSize != 0 && max != Choices.Count)
            {
                ConsoleHelper.WriteLine("[→] Next Page");
            }
        }
    }
}