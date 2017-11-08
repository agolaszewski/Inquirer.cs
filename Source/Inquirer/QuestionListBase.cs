using System.Collections.Generic;

namespace InquirerCS
{
    public abstract class QuestionListBase<T> : QuestionSingleChoiceBase<T>
    {
        internal QuestionListBase(string message) : base(message)
        {
        }

        internal List<T> Choices { get; set; }
    }
}