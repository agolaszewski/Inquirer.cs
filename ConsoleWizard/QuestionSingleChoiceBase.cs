using System;
using ConsoleWizard.Components;

namespace ConsoleWizard
{
    public abstract class QuestionSingleChoiceBase<T> : QuestionBase<T>, IConvertToString<T>
    {
        public QuestionSingleChoiceBase(string question) : base(question)
        {
        }

        public Func<T, string> ConvertToStringFn { get; set; }

        public void DisplayQuestion()
        {
            Console.Clear();
            ConsoleHelper.Write("[?] ", ConsoleColor.Yellow);
            var question = $"{Message} : ";
            if (HasDefaultValue)
            {
                question += $"[{ConvertToStringFn(DefaultValue)}] ";
            }
        }
    }
}