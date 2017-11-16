using System;
using System.Collections.Generic;
using System.Linq;

namespace InquirerCS
{
    public class QuestionCheckbox<TList, TResult> : QuestionMultipleListBase<TList, TResult> where TList : List<TResult>, new()
    {
        public QuestionCheckbox(QuestionCheckbox<TList, TResult> questionCheckbox) : base(questionCheckbox)
        {
        }

        internal QuestionCheckbox(string question) : base(question)
        {
        }

        public QuestionMultipleListBase<TList, TResult> Page(int pageSize)
        {
            return new QuestionPagedCheckbox<TList, TResult>(this, pageSize);
        }

        internal override TList Prompt()
        {
            int _boundryBottom;
            int _boundryTop;

            bool tryAgain = true;
            TList answer = DefaultValue;

            while (tryAgain)
            {
                DisplayQuestion();

                ConsoleHelper.WriteLine();
                ConsoleHelper.WriteLine();
                Console.CursorVisible = false;

                _boundryTop = Console.CursorTop;

                for (int i = 0; i < Choices.Count; i++)
                {
                    DisplayCheckbox(i, 2, i + _boundryTop);
                    ConsoleHelper.PositionWriteLine(DisplayChoice(i), 4, i + _boundryTop);
                }

                _boundryBottom = _boundryTop + Choices.Count - 1;

                ConsoleHelper.PositionWrite("→", 0, _boundryTop);
                ConsoleHelper.PositionWrite(DisplayChoice(0), 4, _boundryTop, ConsoleColor.DarkYellow);

                bool move = true;
                while (move)
                {
                    int y = Console.CursorTop;

                    bool isCanceled = false;
                    var key = ConsoleHelper.ReadKey(out isCanceled);
                    if (isCanceled)
                    {
                        IsCanceled = isCanceled;
                        return default(TList);
                    }

                    DisplayCheckbox(y - _boundryTop, 2, y);
                    ConsoleHelper.PositionWrite(DisplayChoice(y - _boundryTop), 4, y);

                    switch (key)
                    {
                        case (ConsoleKey.RightArrow):
                            {
                                Selected[y - _boundryTop] = !Selected[y - _boundryTop];
                                DisplayCheckbox(y - _boundryTop, 2, y);

                                break;
                            }

                        case (ConsoleKey.UpArrow):
                            {
                                if (y > _boundryTop)
                                {
                                    y -= 1;
                                }

                                break;
                            }

                        case (ConsoleKey.DownArrow):
                            {
                                if (y < _boundryBottom)
                                {
                                    y += 1;
                                }

                                break;
                            }

                        case (ConsoleKey.Enter):
                            {
                                Console.CursorVisible = true;
                                var selectedChoices = new TList();
                                for (int i = 0; i < Selected.Length; i++)
                                {
                                    if (Selected[i])
                                    {
                                        selectedChoices.Add(Choices[i]);
                                    }
                                }

                                if (Validate(selectedChoices))
                                {
                                    answer = selectedChoices;
                                    move = false;
                                }
                                else
                                {
                                    return Prompt();
                                }

                                break;
                            }
                    }

                    ConsoleHelper.PositionWrite(" ", 0, y - 1);
                    ConsoleHelper.PositionWrite("→", 0, y);
                    ConsoleHelper.PositionWrite(DisplayChoice(y - _boundryTop), 4, y, ConsoleColor.DarkYellow);
                    Console.SetCursorPosition(0, y);
                }

                var answerNames = answer.Select(x => ConvertToStringFn(x)).ToList();
                tryAgain = Confirm(string.Join(",", answerNames));
            }

            Console.WriteLine();
            return answer;
        }

        protected void DisplayCheckbox(int selectedIndex, int x, int y)
        {
            if (Selected[selectedIndex])
            {
                ConsoleHelper.PositionWrite("*", x, y);
            }
            else
            {
                ConsoleHelper.PositionWrite(" ", x, y);
            }
        }

        protected virtual string DisplayChoice(int index)
        {
            return $"{ConvertToStringFn(Choices[index])}";
        }
    }
}
