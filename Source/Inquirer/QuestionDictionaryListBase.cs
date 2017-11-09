using System;
using System.Collections.Generic;

namespace InquirerCS
{
    public abstract class QuestionDictionaryListBase<TDictionary, TResult> : QuestionSingleChoiceBase<TResult> where TDictionary : Dictionary<ConsoleKey, TResult>
    {
        protected QuestionDictionaryListBase(string message) : base(message)
        {
        }

        internal TDictionary Choices { get; set; }

        public QuestionDictionaryListBase<TDictionary, TResult> ConvertToString(Func<TResult, string> toStringFn)
        {
            ConvertToStringFn = toStringFn;
            return this;
        }
    }
}