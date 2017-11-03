using System;

namespace ConsoleWizard
{
    public class InquirerPrompt<TAnswers> where TAnswers : class, new()
    {
        private Inquirer<TAnswers> _inquirer;

        public InquirerPrompt(Inquirer<TAnswers> inquirer)
        {
            _inquirer = inquirer;
        }

        public void Then(Action<TAnswers> thenFn)
        {
            if (_inquirer != null)
            {
                thenFn(_inquirer.Answers);
            }
        }
    }
}