using ConsoleWizard;
using System;

namespace ConsoleApp1
{
    internal class Program
    {
        private static Inquirer<Answers> _test;

        private static void Main(string[] args)
        {
            _test = new Inquirer<Answers>();
            ConfirmTest();
            Console.WriteLine(_test.Answers.One);
        }

        private static void ConfirmTest()
        {
            _test.For(x => x.One).Prompt(Question.Confirm("Are you sure?")).Prompt();
        }

        private static void Herp()
        {
            _test.For(x => x.One).Prompt(Question.ConsoleKeyInput("[[W]] or [[A]]", ConsoleKey.A, ConsoleKey.W)).Then(answers =>
            {
                if (answers.One == ConsoleKey.A)
                {
                    Herp();
                }
            });
        }
    }
}