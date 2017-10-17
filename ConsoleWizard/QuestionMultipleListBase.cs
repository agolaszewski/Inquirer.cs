using System;
using System.Collections.Generic;
using ConsoleWizard.Components;

namespace ConsoleWizard
{
    public abstract class QuestionMultipleListBase<TList, T> : QuestionBase<TList>, IConvertToString<T> where TList : List<T>
    {
        private TList _choices;

        internal QuestionMultipleListBase(string message) : base(message)
        {
        }

        public Func<T, string> ConvertToStringFn { get; set; } = value => { return value.ToString(); };

        internal TList Choices
        {
            get
            {
                return _choices;
            }

            set
            {
                _choices = value;
                Selected = new bool[value.Count];
            }
        }

        internal bool[] Selected { get; private set; }

        public void DisplayQuestion()
        {
            Console.Clear();
            ConsoleHelper.Write("[?] ", ConsoleColor.Yellow);
            var question = $"{Message} : ";
            if (HasDefaultValue)
            {
                foreach (var item in DefaultValue)
                {
                    question += $"[{ConvertToStringFn(item)}] ";
                }
            }
        }
    }
}