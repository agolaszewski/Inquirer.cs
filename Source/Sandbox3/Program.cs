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
            Question.Prompt(Question.Input(name)).Then(ref test);
        }

        private static void SetClientActiveStatus()
        {
            Question.Prompt(Question.Menu("ASdasda").AddOption("asdasda", () => { XXXX("1"); }));

            XXXX("1");
            Question.Prompt(Question.Input("2")).Then(x =>
            {
                Question.Prompt(Question.Input("2.1")).Then(answer =>
                {
                    XXXX("2.1.1");
                    XXXX("2.1.2");
                    XXXX("2.1.3");
                });
                Question.Prompt(Question.Input("2.2")).Then(answer =>
                {
                    XXXX("2.2.1");
                    XXXX("2.2.2");
                    XXXX("2.2.3");
                });
            });
            XXXX("3");
            XXXX("4");
            XXXX("5");
            XXXX("6");
        }
    }
}