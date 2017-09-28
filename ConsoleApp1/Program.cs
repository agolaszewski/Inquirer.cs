using ConsoleWizard;
using System;
using System.Collections.Generic;

namespace ConsoleApp1
{
    internal class Program
    {
        private static Inquirer<Answers> _test;

        private static void Main(string[] args)
        {
            _test = new Inquirer<Answers>();
            ListTest();
            Console.WriteLine(_test.Answers.Two);
        }

        private static void ConfirmTest()
        {
            _test.For(x => x.One).Prompt(Question.Confirm("Are you sure?")).Prompt();
        }

        private static void ListTest()
        {
            var list = new List<ConsoleColor> { ConsoleColor.Yellow, ConsoleColor.Magenta, ConsoleColor.DarkYellow };
            _test.For(x => x.Two).Prompt(Question.RawList("Choose color?", list).WithDefaultValue(ConsoleColor.DarkYellow)).Prompt();
        }

        private static void Herp()
        {
            _test.For(x => x.One).Prompt(Question.ConsoleKey("[[W]] or [[A]]", ConsoleKey.A, ConsoleKey.W)).Then(answers =>
            {
                if (answers.One == ConsoleKey.A)
                {
                    Herp();
                }
            });
        }
    }
}