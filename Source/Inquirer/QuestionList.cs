using System;

namespace InquirerCS
{
    public class QuestionList<TResult> : QuestionListBase<TResult>
    {
        internal QuestionList(string question) : base(question)
        {
        }

        protected QuestionList(QuestionList<TResult> questionList) : base(questionList.Message)
        {
            Choices = questionList.Choices;
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

                ConsoleHelper.WriteLine(DisplayChoice(0), ConsoleColor.DarkYellow);
                for (int i = 1; i < Choices.Count; i++)
                {
                    ConsoleHelper.WriteLine(DisplayChoice(i));
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

                    Console.SetCursorPosition(0, y);
                    ConsoleHelper.Write(DisplayChoice(y - boundryTop));
                    Console.SetCursorPosition(0, y);

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

                    ConsoleHelper.PositionWrite(DisplayChoice(y - boundryTop), 0, y, ConsoleColor.DarkYellow);
                    ConsoleHelper.PositionWrite("→", 0, y);
                    Console.SetCursorPosition(0, y);
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