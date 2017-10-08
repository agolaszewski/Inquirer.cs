using System.Collections.Generic;

namespace ConsoleWizard
{
    public abstract class QuestionListBase<T> : QuestionBase<T>
    {
        public QuestionListBase(string message) : base(message)
        {
        }

        public List<T> Choices { get; set; }
    }
}
