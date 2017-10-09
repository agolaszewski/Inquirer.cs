using System.Collections.Generic;

namespace ConsoleWizard
{
    public abstract class QuestionListBase<T> : QuestionBase<T>
    {
        internal QuestionListBase(string message) : base(message)
        {
        }

        internal List<T> Choices { get; set; }
    }
}
