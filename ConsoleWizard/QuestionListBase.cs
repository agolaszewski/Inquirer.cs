using System;
using System.Collections.Generic;

namespace ConsoleWizard
{
    public abstract class QuestionListBase<T> : QuestionBase<T>
    {
        internal QuestionListBase(string message) : base(message)
        {
        }

        public Func<T, string> ToStringFn { get; set; } = value => { return value.ToString(); };

        internal List<T> Choices { get; set; }

        public QuestionListBase<T> ToString(Func<T, string> toStringFn)
        {
            ToStringFn = toStringFn;
            return this;
        }
    }
}
