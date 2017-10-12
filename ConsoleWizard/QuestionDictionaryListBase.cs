using System;
using System.Collections.Generic;

namespace ConsoleWizard
{
    public abstract class QuestionDictionaryListBase<TDictionary, T> : QuestionBase<T> where TDictionary : Dictionary<ConsoleKey, T>
    {
        protected QuestionDictionaryListBase(string message) : base(message)
        {
        }

        public Func<T, string> ToStringFn { get; set; } = value => { return value.ToString(); };

        internal TDictionary Choices { get; set; }
    }
}
