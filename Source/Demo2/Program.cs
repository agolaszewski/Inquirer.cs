using InquirerCS;

namespace Demo2
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            string answer = string.Empty;
            Inquirer.Prompt(Question.Input("1")).Bind(() => answer);
            Inquirer.Prompt(Question.Input("2")).Bind(() => answer);
            Inquirer.Prompt(() =>
            {
                if (answer.Length > 5)
                {
                    return Question.Input("2");
                }

                return null;
            }).Then(answer2 =>
            {
                Inquirer.Prompt(Question.Input("2.1")).Bind(() => answer);
                Inquirer.Prompt(Question.Input("2.2")).Bind(() => answer);
                Inquirer.Prompt(Question.Input("2.3")).Bind(() => answer);
            });
            Inquirer.Go();
        }
    }
}