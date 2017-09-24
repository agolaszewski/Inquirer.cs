namespace ConsoleWizard
{
    public class InquirerFor<TAnswers, TResult> : Inquirer<TAnswers> where TAnswers : class, new()
    {
        private Inquirer<TAnswers> _inquirer;

        public InquirerFor(Inquirer<TAnswers> inquirer)
        {
            _inquirer = inquirer;
        }

        public InquirerPrompt<TAnswers> Prompt(QuestionBase<TResult> question)
        {
            _inquirer.CurrentQuestion.Action = v => { v.SetValue(Answers, question.Prompt()); };
            return new InquirerPrompt<TAnswers>(_inquirer);
        }
    }
}