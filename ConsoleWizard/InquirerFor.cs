using System.Diagnostics;

namespace ConsoleWizard
{
    public class InquirerFor<TAnswers, TResult> where TAnswers : class, new()
    {
        private Inquirer<TAnswers> _inquirer;

        public InquirerFor(Inquirer<TAnswers> inquirer)
        {
            _inquirer = inquirer;
        }

        public InquirerPrompt<TAnswers> Prompt(QuestionBase<TResult> question)
        {
            StackTrace stackTrace = new StackTrace();
            StackFrame[] stackFrames = stackTrace.GetFrames();
            StackFrame callingFrame = stackFrames[1];

            var answer = question.Prompt();
            if (question.IsCanceled)
            {
                if (_inquirer.History.Count > 0)
                {
                    var method = _inquirer.History.Pop();
                    method.Invoke(null, null);
                }
            }
            else
            {
                _inquirer.PropertyInfo.SetValue(_inquirer.Answers, answer);
                _inquirer.History.Push(callingFrame.GetMethod());
            }

            return new InquirerPrompt<TAnswers>(_inquirer);
        }
    }
}