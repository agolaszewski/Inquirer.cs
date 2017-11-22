using System;
using System.Collections.Generic;

namespace InquirerCS
{
    public abstract class QuestionDictionaryListBase<TDictionary, TResult> : QuestionSingleChoiceBase<ConsoleKey, ConsoleKey, TResult> where TDictionary : Dictionary<ConsoleKey, TResult>
    {
        protected QuestionDictionaryListBase(string message) : base(message)
        {
        }

        internal TDictionary Choices { get; set; }
    }
}