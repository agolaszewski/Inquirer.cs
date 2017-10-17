using System;
using System.Collections.Generic;
using ConsoleWizard.Components;

namespace ConsoleWizard
{
    public abstract class QuestionDictionaryListBase<TDictionary, T> : QuestionBase<T>, IConvertToString<T> where TDictionary : Dictionary<ConsoleKey, T>
    {
        protected QuestionDictionaryListBase(string message) : base(message)
        {
        }

        public Func<T, string> ConvertToStringFn { get; set; } = value => { return value.ToString(); };

        internal TDictionary Choices { get; set; }

        public QuestionDictionaryListBase<TDictionary, T> ToString(Func<T, string> toStringFn)
        {
            ConvertToStringFn = toStringFn;
            return this;
        }
    }
}