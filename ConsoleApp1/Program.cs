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
            //InputTest();
            //InputTestNumber();
            ConfirmTest();
            Console.WriteLine(_test.Answers.Input);
            Console.ReadKey();
        }

        private static void ConfirmTest()
        {
            _test.For(x => x.One).Prompt(Question.Confirm("Are you sure?"));
        }

        private static void InputTest()
        {
            _test.For(x => x.Input).Prompt(Question.Input("How are you?"));
        }

        private static void InputTestNumber()
        {
            _test.For(x => x.InputNumber).Prompt(Question.Input<int>("2+2"));
        }

        private static void ListTest()
        {
            var list = new List<ConsoleColor> { ConsoleColor.Yellow, ConsoleColor.Magenta, ConsoleColor.DarkYellow };
            _test.For(x => x.Two).Prompt(Question.List("Choose color?", list).Paging(2).WithDefaultValue(ConsoleColor.DarkBlue));
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