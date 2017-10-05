using System;
using System.Collections.Generic;

namespace ConsoleWizard
{
    public abstract class QuestionListBase<T> : QuestionBase<T>
    {
        public List<T> Choices { get; set; }

        public QuestionListBase(string message) : base(message)
        {
        }
    }

    public abstract class QuestionMultipleListBase<TList, T> : QuestionBase<TList> where TList : List<T>
    {
        public TList Choices { get; set; }

        public QuestionMultipleListBase(string message) : base(message)
        {
        }
    }

    public abstract class QuestionDictionaryListBase<TDictionary, T> : QuestionBase<T> where TDictionary : Dictionary<ConsoleKey, T>
    {
        public TDictionary Choices { get; set; }

        public QuestionDictionaryListBase(string message) : base(message)
        {
        }
    }
}