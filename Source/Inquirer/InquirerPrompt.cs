using System.Diagnostics;

namespace InquirerCS
{
    public class InquirerPrompt<TAnswers, TResult> where TAnswers : class, new()
    {
        private Inquirer<TAnswers> _inquirer;

        private TResult _result;

        public InquirerPrompt(Inquirer<TAnswers> inquirer)
        {
            _inquirer = inquirer;
        }

        private InquirerPrompt(Inquirer<TAnswers> inquirer, TResult result) : this(inquirer)
        {
            _result = result;
        }

        public InquirerFor<TAnswers, TResult> Prompt(QuestionBase<TResult> question)
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

                return new InquirerFor<TAnswers, TResult>(_inquirer, default(TResult));
            }
            else
            {
                _inquirer.History.Push(callingFrame.GetMethod());
            }

            return new InquirerFor<TAnswers, TResult>(_inquirer, answer);
        }
    }
}
