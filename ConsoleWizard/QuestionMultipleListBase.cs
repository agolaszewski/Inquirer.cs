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

        public Func<T, string> ToStringFn { get; set; } = value => { return value.ToString(); };

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
    }
}