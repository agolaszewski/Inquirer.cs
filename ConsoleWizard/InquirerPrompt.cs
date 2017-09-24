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
            _inquirer.CurrentQuestion.Action(_inquirer.CurrentQuestion.PropertyInfo);
            thenFn(_inquirer.Answers);
        }
    }
}