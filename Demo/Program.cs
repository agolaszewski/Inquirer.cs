using ConsoleWizard;

namespace Demo
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var wizard = new Wizard<Answers>();
            wizard.AddQuestionFor<QuestionOne>(x => x.One);
        }
    }
}