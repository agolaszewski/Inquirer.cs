using System;

namespace InquirerCS
{
    public class QuestionList<TResult> : QuestionListBase<TResult>
    {
        internal QuestionList(string question) : base(question)
        {
        }

        protected QuestionList(QuestionList<TResult> questionList) : base(questionList)
        {
        }

        public override QuestionListBase<TResult> Page(int pageSize)
        {
            return new QuestionPagedList<TResult>(this, pageSize);
        }

        internal override TResult Prompt()
        {
            bool tryAgain = true;
            TResult answer = DefaultValue;

            while (tryAgain)
            {
                DisplayQuestion();

                Console.WriteLine();
                Console.WriteLine();

                if (Choices.Count == 0)
                {
                    ConsoleHelper.WriteError("No options to select");
                    Console.ReadKey();
                    IsCanceled = true;
                    return default(TResult);
                }

                ConsoleHelper.PositionWriteLine(DisplayChoice(0), 2, 2, ConsoleColor.DarkYellow);
                for (int i = 1; i < Choices.Count; i++)
                {
                    ConsoleHelper.PositionWriteLine(DisplayChoice(i), 2, i + 2);
                }

                Console.CursorVisible = false;

                int boundryTop = Console.CursorTop - Choices.Count;
                int boundryBottom = boundryTop + Choices.Count - 1;

                ConsoleHelper.PositionWrite("→", 0, boundryTop);

                bool move = true;
                do
                {
                    int y = Console.CursorTop;

                    bool isCanceled = false;
                    var key = ConsoleHelper.ReadKey(out isCanceled);
                    if (isCanceled)
                    {
                        IsCanceled = isCanceled;
                        return default(TResult);
                    }

                    ConsoleHelper.PositionWrite(" ", 0, y);
                    ConsoleHelper.PositionWriteLine(DisplayChoice(y - boundryTop), 2, y);

                    switch (key)
                    {
                        case (ConsoleKey.UpArrow):
                            {
                                if (y > boundryTop)
                                {
                                    y -= 1;
                                }

                                break;
                            }

                        case (ConsoleKey.DownArrow):
                            {
                                if (y < boundryBottom)
                                {
                                    y += 1;
                                }

                                break;
                            }

                        case (ConsoleKey.Enter):
                            {
                                Console.CursorVisible = true;
                                answer = Choices[Console.CursorTop - boundryTop];
                                move = false;
                                break;
                            }
                    }

                    ConsoleHelper.PositionWrite(DisplayChoice(y - boundryTop), 2, y, ConsoleColor.DarkYellow);
                    ConsoleHelper.PositionWrite("→", 0, y);
                }
                while (move);

                tryAgain = Confirm(ConvertToStringFn(answer));
            }

            Console.WriteLine();
            return answer;
        }

        protected virtual string DisplayChoice(int index)
        {
            return $"{ConvertToStringFn(Choices[index])}";
        }
    }
}