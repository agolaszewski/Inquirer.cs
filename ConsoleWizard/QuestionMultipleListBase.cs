using System.Collections.Generic;

namespace ConsoleWizard
{
    public abstract class QuestionMultipleListBase<TList, T> : QuestionBase<TList> where TList : List<T>
    {
        public QuestionMultipleListBase(string message) : base(message)
        {
        }

        public TList Choices { get; set; }
    }
}
