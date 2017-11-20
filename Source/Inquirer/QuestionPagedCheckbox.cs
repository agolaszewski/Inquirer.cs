using System;
using System.Collections.Generic;
using System.Linq;

namespace InquirerCS
{
    public class QuestionPagedCheckbox<TList, TResult> : QuestionCheckbox<TList, TResult> where TList : List<TResult>, new()
    {
        private const int _BOUNDRY_TOP = 2;

        private int _skipChoices;

        internal QuestionPagedCheckbox(QuestionCheckbox<TList, TResult> questionCheckbox, int pageSize) : base(questionCheckbox)
        {
            PageSize = pageSize;
        }

        internal int PageSize { get; private set; }

        public override TList Prompt()
        {
            return Prompt(_BOUNDRY_TOP);
        }

        private TList Prompt(int cursorPosition)
        {
            int y = cursorPosition;
            int _boundryBottom;

            bool tryAgain = true;
            TList answer = DefaultValue;

            Console.Clear();

            while (tryAgain)
            {
                DisplayQuestion();

                ConsoleHelper.WriteLine();
                ConsoleHelper.WriteLine();
                Console.CursorVisible = false;

                for (int i = _skipChoices; i < MathHelper.Clamp(_skipChoices + PageSize, 0, Choices.Count); i++)
                {
                    DisplayCheckbox(i, 2, _BOUNDRY_TOP + (i % PageSize));
                    ConsoleHelper.PositionWriteLine(DisplayChoice(i), 4, _BOUNDRY_TOP + (i % PageSize));
                }

                ConsoleHelper.PositionWrite($"Page {Math.Ceiling(((double)_skipChoices / (double)PageSize) + 1)} of {Math.Ceiling((double)Choices.Count / (double)PageSize)}", y: Console.CursorTop + 1);
                Console.SetCursorPosition(0, Console.CursorTop - 1);

                _boundryBottom = MathHelper.Clamp(_skipChoices + PageSize, 0, Choices.Count) - _skipChoices + _BOUNDRY_TOP - 1;

                ConsoleHelper.PositionWrite("→", 0, y);
                ConsoleHelper.PositionWrite(DisplayChoice(y - _BOUNDRY_TOP + _skipChoices), 4, y, ConsoleColor.DarkYellow);

                bool move = true;
                while (move)
                {
                    var key = ConsoleHelper.ReadKey();

                    DisplayCheckbox(y - _BOUNDRY_TOP + _skipChoices, 2, y);
                    ConsoleHelper.PositionWrite(" ", 0, y);
                    ConsoleHelper.PositionWrite(DisplayChoice(y - _BOUNDRY_TOP + _skipChoices), 4, y);

                    switch (key)
                    {
                        case (ConsoleKey.RightArrow):
                            {
                                Selected[y - _BOUNDRY_TOP + _skipChoices] = !Selected[y - _BOUNDRY_TOP + _skipChoices];
                                DisplayCheckbox(y - _BOUNDRY_TOP + _skipChoices, 2, y);

                                break;
                            }

                        case (ConsoleKey.UpArrow):
                            {
                                if (y > _BOUNDRY_TOP)
                                {
                                    y -= 1;
                                }
                                else
                                {
                                    if (_skipChoices - PageSize >= 0)
                                    {
                                        _skipChoices -= PageSize;
                                        return Prompt(PageSize + 1);
                                    }
                                }

                                break;
                            }

                        case (ConsoleKey.DownArrow):
                            {
                                if (y < _boundryBottom)
                                {
                                    y += 1;
                                }
                                else
                                {
                                    if (_skipChoices + PageSize < Choices.Count)
                                    {
                                        _skipChoices += PageSize;
                                        return Prompt(_BOUNDRY_TOP);
                                    }
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
                                    return Prompt(_BOUNDRY_TOP);
                                }

                                break;
                            }
                    }

                    ConsoleHelper.PositionWrite("→", 0, y);
                    ConsoleHelper.PositionWrite(DisplayChoice(y - _BOUNDRY_TOP + _skipChoices), 4, y, ConsoleColor.DarkYellow);
                    Console.SetCursorPosition(0, y);
                }

                var answerNames = answer.Select(x => ConvertToStringFn(x)).ToList();
                tryAgain = Confirm(string.Join(",", answerNames));
            }

            Console.WriteLine();
            return answer;
        }
    }
}