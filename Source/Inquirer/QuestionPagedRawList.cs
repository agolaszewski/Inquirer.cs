using System;

namespace InquirerCS
{
    internal class QuestionPagedRawList<TResult> : QuestionRawList<TResult>
    {
        private int _skipChoices = 0;

        internal QuestionPagedRawList(QuestionRawList<TResult> questionRawList, int pageSize) : base(questionRawList)
        {
            PageSize = pageSize;
        }

        internal int PageSize { get; private set; } = 0;

        public override TResult Prompt()
        {
            bool tryAgain = true;
            TResult answer = DefaultValue;

            while (tryAgain)
            {
                Console.Clear();

                DisplayQuestion();

                Console.WriteLine();
                Console.WriteLine();

                for (int i = _skipChoices; i < MathHelper.Clamp(_skipChoices + PageSize, 0, Choices.Count); i++)
                {
                    ConsoleHelper.WriteLine(DisplayChoice(i));
                }

                ConsoleHelper.WriteLine("Answer:");
                ConsoleHelper.PositionWrite($"Page {Math.Ceiling(((double)_skipChoices / (double)PageSize) + 1)} of {Math.Ceiling((double)Choices.Count / (double)PageSize)}", y: Console.CursorTop + 1);
                Console.SetCursorPosition(8, Console.CursorTop - 2);

                while (true)
                {
                    ConsoleKey? interrupted = null;
                    var value = ReadFn();

                    if (interrupted.HasValue)
                    {
                        switch (interrupted.Value)
                        {
                            case (ConsoleKey.LeftArrow):
                                {
                                    if (_skipChoices - PageSize >= 0)
                                    {
                                        _skipChoices -= PageSize;
                                    }

                                    return Prompt();
                                }

                            case (ConsoleKey.RightArrow):
                                {
                                    if (_skipChoices + PageSize < Choices.Count)
                                    {
                                        _skipChoices += PageSize;
                                    }

                                    return Prompt();
                                }
                        }
                    }
                    else
                    {
                        var parsedValue = value.ToN<int>();
                        if (!parsedValue.HasValue)
                        {
                            tryAgain = true;
                            ConsoleHelper.WriteError($"Cannot parse {value} to {typeof(int)}");
                            return Prompt();
                        }
                        else
                        if (!Validate(parsedValue.Value))
                        {
                            return Prompt();
                        }

                        answer = ParseFn(parsedValue.Value);
                        tryAgain = Confirm(ConvertToStringFn(answer));
                        break;
                    }
                }
            }

            Console.WriteLine();
            return answer;
        }
    }
}