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
            Herp();
        }

        private static void Hur()
        {
            _test.For(x => x.One).Prompt(Question.ConsoleKeyInput("[[Z]] or [[A]]", ConsoleKey.Z, ConsoleKey.A)).Then(answers =>
            {
                Herp();
            });
        }

        private static void Herp()
        {
            _test.For(x => x.One).Prompt(Question.ConsoleKeyInput("[[W]] or [[A]]", ConsoleKey.A, ConsoleKey.W)).Then(answers =>
            {
                if (answers.One == ConsoleKey.A)
                {
                    Herp();
                }
                Hur();
            });
        }
    }
}