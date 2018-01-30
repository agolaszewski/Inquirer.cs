using InquirerCS;

namespace ConsoleApp1
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            SetClientActiveStatus();
        }

        public static void XXXX(string name)
        {
            string test = string.Empty;
            Inquirer.Prompt(Question.Input(name).WithDefaultValue("Menu test")).Then(ref test);
        }

        private static void SetClientActiveStatus()
        {
            //Inquirer.Prompt(Question.Menu("ASdasda").AddOption("asdasda", () => { XXXX("1"); }));
            XXXX("3");
            XXXX("3");
            XXXX("4");
            XXXX("5");
            XXXX("6");
        }
    }
}