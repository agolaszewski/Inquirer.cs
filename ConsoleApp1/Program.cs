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
            _test = new Inquirer<Answers>(_answers);
            MenuTest();
            Console.ReadKey();
        }

        private static void MenuTest()
        {
            _test.Menu("Choose")
               .AddOption("InputTest", () => { InputTest(); })
               .AddOption("InputTestNumber", () => { InputTestNumber(); })
               .AddOption("PasswordTest", () => { PasswordTest(); })
               .AddOption("ListTest", () => { ListTest(); })
               .AddOption("ListRawTest", () => { ListRawTest(); })
               .AddOption("ListCheckboxTest", () => { ListCheckboxTest(); })
               .AddOption("ListExtendedTest", () => { ListExtendedTest(); })
               .AddOption("ConfirmTest", () => { ConfirmTest(); }).Prompt();
        }

        private static void InputTest()
        {
            _test.Prompt(Question.Input("How are you?").WithConfirmation().WithDefaultValue("fine")).For(x => x.Input);
            InputTestNumber();
        }

        private static void InputTestNumber()
        {
            _test.Prompt(Question.Input<int>("2+2").WithConfirmation().WithDefaultValue(4));
            ConfirmTest();
        }

        private static void ConfirmTest()
        {
            _test.Prompt(Question.Confirm("Are you sure?").WithDefaultValue(true).WithConfirmation());
            PasswordTest();
        }

        private static void PasswordTest()
        {
            _test.Prompt(Question.Password("Type password").WithDefaultValue("123456").WithConfirmation());
            ListTest();
        }

        private static void ListTest()
        {
            var list = Enum.GetValues(typeof(ConsoleColor)).Cast<ConsoleColor>().ToList();
            _test.Prompt(Question.List("Choose favourite color", list).WithDefaultValue(ConsoleColor.DarkYellow).WithConfirmation());
            ListRawTest();
        }

        private static void ListRawTest()
        {
            var list = Enum.GetValues(typeof(ConsoleColor)).Cast<ConsoleColor>().ToList();
            _test.Prompt(Question.RawList("Choose favourite color", list).WithDefaultValue(ConsoleColor.DarkRed).WithConfirmation());
            ListCheckboxTest();
        }

        private static void ListCheckboxTest()
        {
            var list = Enum.GetValues(typeof(ConsoleColor)).Cast<ConsoleColor>().ToList();
            _test.Prompt(Question.Checkbox("Choose favourite colors", list).WithDefaultValue(ConsoleColor.DarkGray).WithConfirmation());
            ListExtendedTest();
        }

        private static void ListExtendedTest()
        {
            var list = new Dictionary<ConsoleKey, ConsoleColor>();
            list.Add(ConsoleKey.B, ConsoleColor.Blue);
            list.Add(ConsoleKey.C, ConsoleColor.Cyan);
            list.Add(ConsoleKey.D, ConsoleColor.DarkBlue);

            _test.Prompt(Question.ExtendedList("Choose favourite color", list).WithDefaultValue(ConsoleColor.DarkBlue).WithConfirmation());
        }
    }
}