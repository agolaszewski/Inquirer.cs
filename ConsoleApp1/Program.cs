using System;
using System.Collections.Generic;
using System.Linq;
using ConsoleWizard;

namespace ConsoleApp1
{
    internal class Program
    {
        private static Inquirer<Answers> _test;
        private static Answers _answers = new Answers();

        private static void Main(string[] args)
        {
            //Console.ReadLine();
            _test = new Inquirer<Answers>(_answers);
            InputTest();
            //InputTest();
            //InputTestNumber();
            //ConfirmTest();
            //PasswordTest();
            //ListTest();
            //ListRawTest();
            //ListCheckboxTest();
            //ListExtendedTest();
            //MenuTest();
            Console.WriteLine(_test.Answers.Input);
            Console.ReadKey();
        }

        private static void MenuTest()
        {
            _test.Menu("Choose")
               .AddOption("InputTest", () => { InputTest(); })
               .AddOption("InputTestNumber", () => { InputTestNumber(); })
               .AddOption("ConfirmTest", () => { ConfirmTest(); }).Prompt();
        }

        private static void InputTest()
        {
            _test.Prompt(Question.Input("How are you?").WithConfirmation()).For(x => x.Input);
            InputTestNumber();
        }

        private static void InputTestNumber()
        {
            _test.Prompt(Question.Input<int>("2+2"));
            ConfirmTest();
        }

        private static void ConfirmTest()
        {
            _test.Prompt(Question.Confirm("Are you sure?").WithDefaultValue(true).WithConfirmation());
            PasswordTest();
        }

        private static void PasswordTest()
        {
            _test.Prompt(Question.Password("How are you?").WithDefaultValue("123456").WithConfirmation());
            ListTest();
        }

        private static void ListTest()
        {
            var list = Enum.GetValues(typeof(ConsoleColor)).Cast<ConsoleColor>().ToList();
            _test.Prompt(Question.List("Choose color?", list).WithDefaultValue(ConsoleColor.DarkYellow).WithConfirmation());
            ListRawTest();
        }

        private static void ListRawTest()
        {
            var list = Enum.GetValues(typeof(ConsoleColor)).Cast<ConsoleColor>().ToList();
            _test.Prompt(Question.RawList("Choose color?", list).WithDefaultValue(ConsoleColor.DarkRed).WithConfirmation());
            ListCheckboxTest();
        }

        private static void ListCheckboxTest()
        {
            var list = Enum.GetValues(typeof(ConsoleColor)).Cast<ConsoleColor>().ToList();
            _test.Prompt(Question.Checkbox("Chose favourite colors", list).WithDefaultValue(ConsoleColor.DarkGray).WithConfirmation());
            ListExtendedTest();
        }

        private static void ListExtendedTest()
        {
            var list = new Dictionary<ConsoleKey, ConsoleColor>();
            list.Add(ConsoleKey.B, ConsoleColor.Blue);
            list.Add(ConsoleKey.C, ConsoleColor.Cyan);
            list.Add(ConsoleKey.D, ConsoleColor.DarkBlue);

            _test.Prompt(Question.ExtendedList("sdada", list).WithDefaultValue(ConsoleColor.DarkBlue).WithConfirmation());
        }
    }
}