using System;
using System.Collections.Generic;

namespace InquirerCS
{
    public abstract class QuestionDictionaryListBase<TDictionary, T> : QuestionSingleChoiceBase<T> where TDictionary : Dictionary<ConsoleKey, T>
    {
        protected QuestionDictionaryListBase(string message) : base(message)
        {
        }

        internal TDictionary Choices { get; set; }

        public QuestionDictionaryListBase<TDictionary, T> ToString(Func<T, string> toStringFn)
        {
            ConvertToStringFn = toStringFn;
            return this;
        }
    }
}