using System;
using InquirerCS;

namespace ConsoleApp1
{
    public class Answers
    {
        public string Test { get; set; }
    }

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
                if (test.Length > -1)
                {
                    return Question.Input(name).WithDefaultValue("Menu test");
                }
                return null;
            }).Then(answer => test = answer);
        }

        private static void SetClientActiveStatus()
        {
            var aaaa = new Answers();
            string test = string.Empty;

            Inquirer.Prompt(Question.Input("2")).Bind(() => aaaa.Test);
            Inquirer.Prompt(Question.Input("2")).Bind(() => test);
            Inquirer.Go();
            Console.WriteLine(aaaa.Test);
        }
    }
}