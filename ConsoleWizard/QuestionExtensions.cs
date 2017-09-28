using System;
using System.Linq;

namespace ConsoleWizard
{
    public static class QuestionExtensions
    {
        public static QuestionBase<T> WithConfirmation<T>(this QuestionBase<T> question)
        {
            question.HasConfirmation = true;
            return question;
        }

        public static QuestionBase<T> WithDefaultValue<T>(this QuestionBase<T> question, T defaultValue)
        {
            question.DefaultValue = defaultValue;
            question.HasDefaultValue = true;
            return question;
        }

        public static QuestionBase<T> WithDefaultValue<T>(this QuestionRawList<T> question, T defaultValue) where T : IComparable
        {
            if (question.Choices.Where(x => x.CompareTo(defaultValue) == 0).Any())
            {
                var index = question.Choices.Select((v, i) => new { Value = v, Index = i }).First(x => x.Value.CompareTo(defaultValue) == 0).Index;
                question.Choices.Insert(0, question.Choices[index]);
                question.Choices.RemoveAt(index + 1);

                question.DefaultValue = question.Choices[0];
                question.HasDefaultValue = true;
            }
            else
            {
                question.Choices.Insert(0, defaultValue);
                question.DefaultValue = defaultValue;
                question.HasDefaultValue = true;
            }
            return question;
        }

        public static QuestionBase<T> WithDefaultValue<T>(this QuestionRawList<T> question, int index)
        {
            question.DefaultValue = question.Choices[index];
            question.HasDefaultValue = true;
            return question;
        }

        //public static QuestionRawList<T> WithNavigation<T>(this QuestionRawList<T> question)
        //{
        //    question.DisplayQuestionAnswersFn = (index, choice) =>
        //    {
        //        Console.WriteLine($"  {choice.ToString()}");
        //    };

        //    question.ReadFn = () =>
        //    {
        //        Console.CursorVisible = false;

        //        int boundryTop = Console.CursorTop - question.Choices.Count;
        //        int boundryBottom = boundryTop + question.Choices.Count - 1;

        //        while (true)
        //        {
        //            int y = Console.CursorTop;
        //            var key = Console.ReadKey().Key;

        //            switch (key)
        //            {
        //                case (ConsoleKey.UpArrow):
        //                    {
        //                        if (y > boundryTop)
        //                        {
        //                            y -= 1;
        //                        }
        //                        break;
        //                    }
        //                case (ConsoleKey.DownArrow):
        //                    {
        //                        if (y < boundryBottom)
        //                        {
        //                            y += 1;
        //                        }
        //                        break;
        //                    }
        //                case (ConsoleKey.Enter):
        //                    {
        //                        Console.CursorVisible = true;
        //                        return Console.CursorTop - boundryTop;
        //                    }
        //            }

        //            Console.SetCursorPosition(0, y);
        //            Console.Write("→");
        //            Console.SetCursorPosition(0, y);
        //        }
        //    };

        //    return question;
        //}
    }
}