using System;
using System.Diagnostics;

namespace ConsoleWizard
{
    public class InquirerConvert<TAnswers, TAnswer, TResult> where TAnswers : class, new()
    {
        private QuestionBase<TAnswer> _question;
        private Inquirer<TAnswers> _inquirer;

        public InquirerConvert(Inquirer<TAnswers> inquirer, QuestionBase<TAnswer> question)
        {
            _inquirer = inquirer;
            _question = question;
        }

        public InquirerPrompt<TAnswers> Convert(Func<TAnswer, TResult> parseFn)
        {
            StackTrace stackTrace = new StackTrace();
            StackFrame[] stackFrames = stackTrace.GetFrames();
            StackFrame callingFrame = stackFrames[1];

            var answer = _question.Prompt();
            if (_question.IsCanceled)
            {
                if (_inquirer.History.Count > 0)
                {
                    var method = _inquirer.History.Pop();
                    method.Invoke(null, null);
                }
            }

            _inquirer.History.Push(callingFrame.GetMethod());
            _inquirer.PropertyInfo.SetValue(_inquirer.Answers, parseFn(answer));

            return new InquirerPrompt<TAnswers>(_inquirer);
        }
    }
}