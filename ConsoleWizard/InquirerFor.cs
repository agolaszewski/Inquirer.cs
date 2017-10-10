using System.Diagnostics;
using System.Reflection;

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
            _inquirer.History.Push(callingFrame.GetMethod());

            _inquirer.PropertyInfo.SetValue(_inquirer.Answers, question.Prompt());
            return new InquirerPrompt<TAnswers>(_inquirer);
        }
    }
}