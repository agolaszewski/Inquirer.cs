using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;

namespace ConsoleWizard
{
    public class Inquirer<TAnswers> where TAnswers : class, new()
    {
        public Inquirer()
        {
            Answers = new TAnswers();
        }

        public Inquirer(TAnswers answers)
        {
            Answers = answers;
        }

        public TAnswers Answers { get; private set; }

        internal Stack<MethodBase> History { get; set; } = new Stack<MethodBase>();

        internal PropertyInfo PropertyInfo { get; set; }

        public InquirerMenu<TAnswers> Menu(string header)
        {
            return new InquirerMenu<TAnswers>(header, this);
        }

        public InquirerFor<TAnswers, TResult> Prompt<TResult>(QuestionBase<TResult> question)
        {
            StackTrace stackTrace = new StackTrace();
            StackFrame[] stackFrames = stackTrace.GetFrames();
            StackFrame callingFrame = stackFrames[1];

            var answer = question.Prompt();
            if (question.IsCanceled)
            {
                if (History.Count > 0)
                {
                    var method = History.Pop();
                    method.Invoke(null, null);
                }
                else
                {
                    return new InquirerFor<TAnswers, TResult>(this, default(TResult));
                }
            }
            else
            {
                History.Push(callingFrame.GetMethod());
            }

            return new InquirerFor<TAnswers, TResult>(this, answer);
        }
    }
}