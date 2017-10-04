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
            _inquirer.PropertyInfo.SetValue(_inquirer.Answers, question.Prompt());
            return new InquirerPrompt<TAnswers>(_inquirer);
        }
    }
}