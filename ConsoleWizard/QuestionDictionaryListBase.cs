using System;
using System.Collections.Generic;

namespace ConsoleWizard
{
    public abstract class QuestionDictionaryListBase<TDictionary, T> : QuestionBase<T> where TDictionary : Dictionary<ConsoleKey, T>
    {
        public QuestionDictionaryListBase(string message) : base(message)
        {
        }

        public TDictionary Choices { get; set; }
    }
}
