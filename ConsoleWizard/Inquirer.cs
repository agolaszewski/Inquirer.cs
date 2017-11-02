using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq.Expressions;
using System.Reflection;

namespace ConsoleWizard
{
    public class Inquirer<TAnswers> where TAnswers : class, new()
    {
        public Inquirer()
        {
            Answers = new TAnswers();
        }

        public TAnswers Answers { get; private set; }

        internal Stack<MethodBase> History { get; set; } = new Stack<MethodBase>();

        internal PropertyInfo PropertyInfo { get; set; }

        public InquirerFor<TAnswers, TResult> For<TResult>(Expression<Func<TAnswers, TResult>> answerProperty)
        {
            var propertyInfo = ((MemberExpression)answerProperty.Body).Member as PropertyInfo;
            if (propertyInfo == null)
            {
                throw new ArgumentException("The lambda expression 'property' should point to a valid Property");
            }

            PropertyInfo = propertyInfo;

            return new InquirerFor<TAnswers, TResult>(this);
        }

        public TResult Prompt<TResult>(QuestionBase<TResult> question)
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
                    return Prompt(question);
                }
            }
            else
            {
                History.Push(callingFrame.GetMethod());
            }

            return answer;
        }
    }
}