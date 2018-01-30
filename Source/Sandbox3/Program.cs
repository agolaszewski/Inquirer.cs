using InquirerCS;

namespace ConsoleApp1
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            SetClientActiveStatus();
        }

        private static string test = string.Empty;

        public static void XXXX(string name)
        {
            Inquirer.Prompt(() =>
            {
                if (test.Length > 3)
                {
                    return Question.Input(name).WithDefaultValue("Menu test");
                }
                return null;
            }).Then(answer => test = answer);
        }

        private static void SetClientActiveStatus()
        {
            //Inquirer.Prompt(Question.Menu("ASdasda").AddOption("asdasda", () => { XXXX("1"); }));
            XXXX("1");
            Inquirer.Prompt(Question.Input("2")).Then(x =>
            {
                test = x;
                Inquirer.Prompt(Question.Input("2.1")).Then(answer =>
                {
                    XXXX("2.1.1");
                    XXXX("2.1.2");
                    XXXX("2.1.3");
                });
                Inquirer.Prompt(Question.Input("2.2")).Then(answer =>
                {
                    XXXX("2.2.1");
                    XXXX("2.2.2");
                    XXXX("2.2.3");
                });
            });
            XXXX("3");
            Inquirer.Go();
        }
    }
}