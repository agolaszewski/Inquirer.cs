using System;

namespace ConsoleWizard
{
    public abstract class QuestionSingleChoiceBase<T> : QuestionBase<T>
    {
        public QuestionSingleChoiceBase(string question) : base(question)
        {
        }

        public Func<T, string> ConvertToStringFn { get; set; } = value => { return value.ToString(); };

        public void DisplayQuestion()
        {
            Console.Clear();
            ConsoleHelper.Write("[?] ", ConsoleColor.Yellow);
            var question = $"{Message} : ";
            if (HasDefaultValue)
            {
                question += $"[{ConvertToStringFn(DefaultValue)}] ";
            }

            ConsoleHelper.Write(question);
        }
    }
}