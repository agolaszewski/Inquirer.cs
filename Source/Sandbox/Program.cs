using System;
using System.Collections.Generic;
using System.Linq;
using InquirerCS;

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
               .AddOption("PagingCheckboxTest", () => { PagingCheckboxTest(); })
               .AddOption("PagingRawListTest", () => { PagingRawListTest(); })
               .AddOption("PagingListTest", () => { PagingListTest(); })
               .AddOption("ValidationTestNumber", () => { ValidationTestNumber(); })
               .AddOption("InputTest", () => { InputTest(); })
               .AddOption("InputTestNumber", () => { InputTestNumber(); })
               .AddOption("PasswordTest", () => { PasswordTest(); })
               .AddOption("ListTest", () => { ListTest(); })
               .AddOption("ListRawTest", () => { ListRawTest(); })
               .AddOption("ListCheckboxTest", () => { ListCheckboxTest(); })
               .AddOption("ListExtendedTest", () => { ListExtendedTest(); })
               .AddOption("ConfirmTest", () => { ConfirmTest(); }).Prompt();
        }

        private static void PagingCheckboxTest()
        {
            var colors = Enum.GetValues(typeof(ConsoleColor)).Cast<ConsoleColor>().ToList();
            _test.Prompt(Question.Checkbox("Choose favourite colors", colors).Page(14).WithDefaultValue(ConsoleColor.DarkGray).WithConfirmation());
            MenuTest();
        }

        private static void PagingRawListTest()
        {
            var colors = Enum.GetValues(typeof(ConsoleColor)).Cast<ConsoleColor>().ToList();
            _test.Prompt(Question.RawList("Choose favourite color", colors).Page(3).WithDefaultValue(ConsoleColor.DarkYellow).WithConfirmation());
            MenuTest();
        }

        private static void PagingListTest()
        {
            var colors = Enum.GetValues(typeof(ConsoleColor)).Cast<ConsoleColor>().ToList();
            _test.Prompt(Question.List("Choose favourite color", colors).Page(3).WithDefaultValue(ConsoleColor.DarkYellow).WithConfirmation());
            MenuTest();
        }

        private static void InputTest()
        {
            _test.Prompt(Question.Input("How are you?").WithConfirmation().WithDefaultValue("fine")).For(x => x.Input);
            MenuTest();
        }

        private static void InputTestNumber()
        {
            _test.Prompt(Question.Input<int>("2+2").WithConfirmation().WithDefaultValue(4));
            MenuTest();
        }

        private static void ValidationTestNumber()
        {
            _test.Prompt(Question.Input<int>("2+2").WithConfirmation().WithValidation(answer => answer == 4, "Not equal 4"));
            MenuTest();
        }

        private static void ConfirmTest()
        {
            _test.Prompt(Question.Confirm("Are you sure?").WithDefaultValue(true).WithConfirmation());
            MenuTest();
        }

        private static void PasswordTest()
        {
            _test.Prompt(Question.Password("Type password").WithDefaultValue("123456").WithConfirmation());
            MenuTest();
        }

        private static void ListTest()
        {
            var colors = Enum.GetValues(typeof(ConsoleColor)).Cast<ConsoleColor>().ToList();
            _test.Prompt(Question.List("Choose favourite color", colors).WithDefaultValue(ConsoleColor.DarkYellow).WithConfirmation());
            MenuTest();
        }

        private static void ListRawTest()
        {
            var colors = Enum.GetValues(typeof(ConsoleColor)).Cast<ConsoleColor>().ToList();
            _test.Prompt(Question.RawList("Choose favourite color", colors).WithDefaultValue(ConsoleColor.DarkRed).WithConfirmation());
            MenuTest();
        }

        private static void ListCheckboxTest()
        {
            var colors = Enum.GetValues(typeof(ConsoleColor)).Cast<ConsoleColor>().ToList();
            _test.Prompt(Question.Checkbox("Choose favourite colors", colors).WithDefaultValue(ConsoleColor.DarkGray).WithConfirmation());
            MenuTest();
        }

        private static void ListExtendedTest()
        {
            var colors = new Dictionary<ConsoleKey, ConsoleColor>();
            colors.Add(ConsoleKey.B, ConsoleColor.Blue);
            colors.Add(ConsoleKey.C, ConsoleColor.Cyan);
            colors.Add(ConsoleKey.D, ConsoleColor.DarkBlue);

            _test.Prompt(Question.ExtendedList("Choose favourite color", colors).WithDefaultValue(ConsoleColor.DarkBlue).WithConfirmation());
            MenuTest();
        }
    }
}