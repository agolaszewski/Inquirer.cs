using System;
using System.Collections.Generic;
using System.Linq;
using InquirerCS;

namespace ConsoleApp1
{
    internal class TestClass
    {
        public string Name { get; set; }
        public bool IsActive { get; set; }
    }

    internal class Program
    {
        private static Inquirer _test = new Inquirer();

        private static void Main(string[] args)
        {
            _test.Next(() => MenuTest());
            Console.ReadKey();
        }

        private static void MenuTest()
        {
            _test.Menu("Choose")
               .AddOption("PagingCheckboxTest", () => { PagingCheckboxTest2(); })
               .AddOption("PagingRawListTest", () => { PagingRawListTest(); })
               .AddOption("PagingListTest", () => { PagingListTest(); })
               .AddOption("InputTest", () => { InputTest(); })
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
            var answer = _test.Prompt(Question.Checkbox("Choose favourite colors", colors)
                .Page(3)
                .WithDefaultValue(new List<ConsoleColor>() { ConsoleColor.Black, ConsoleColor.DarkGray })
                .WithConfirmation()
                .WithValidation(values => values.Any(item => item == ConsoleColor.Black), "Choose black"));

            _test.Next(() => MenuTest());
        }

        private static void PagingCheckboxTest2()
        {
            var colors = new List<TestClass>() { new TestClass() { Name = "zzzz" }, new TestClass() { IsActive = true, Name = "asdasda" } };

            var answer = _test.Prompt(Question.Checkbox("Choose favourite colors", colors)
                .Page(2)
                .WithDefaultValue(x => { return true; })
                .WithConfirmation()
                .WithConvertToString(item => item.Name));

            _test.Next(() => MenuTest());
        }

        private static void PagingRawListTest()
        {
            var colors = Enum.GetValues(typeof(ConsoleColor)).Cast<ConsoleColor>().ToList();
            var answer = _test.Prompt(Question.RawList("Choose favourite color", colors)
                .Page(3)
                .WithDefaultValue(ConsoleColor.DarkCyan)
                .WithConfirmation()
                .WithValidation(item => item == ConsoleColor.Black, "Choose black"));
            _test.Next(() => MenuTest());
        }

        private static void PagingListTest()
        {
            var colors = Enum.GetValues(typeof(ConsoleColor)).Cast<ConsoleColor>().ToList();
            var answer = _test.Prompt(Question.List("Choose favourite color", colors)
                .Page(3)
                .WithDefaultValue(ConsoleColor.DarkCyan)
                .WithConfirmation()
                .WithValidation(item => item == ConsoleColor.Black, "Choose black"));

            _test.Next(() => MenuTest());
        }

        private static void InputTest2()
        {
            _test.Prompt(Question.Input<int>("2 + 2").WithDefaultValue(4).WithConfirmation().WithValidation(value => value == 4, "Answer not equal 4"));
            _test.Next(() => MenuTest());
        }

        private static void InputTest()
        {
            string answer = _test.Prompt(Question.Input("How are you?")
                .WithDefaultValue("fine")
                .WithConfirmation()
                .WithValidation(value => value == "fine", "You cannot be not fine!"));

            _test.Next(() => MenuTest());
        }

        private static void ConfirmTest()
        {
            var answer = _test.Prompt(Question.Confirm("Are you sure?")
                .WithDefaultValue(false));

            _test.Next(() => MenuTest());
        }

        private static void PasswordTest()
        {
            string answer = _test.Prompt(Question.Password("Type password")
                .WithDefaultValue("123456789")
                .WithConfirmation()
                .WithValidation(value => value.Length >= 8 && value.Length <= 10, "Password length must be between 8-10 characters"));

            _test.Next(() => MenuTest());
        }

        private static void ListTest()
        {
            var colors = Enum.GetValues(typeof(ConsoleColor)).Cast<ConsoleColor>().ToList();
            var answer = _test.Prompt(Question.List("Choose favourite color", colors)
                 .WithDefaultValue(ConsoleColor.DarkCyan)
                 .WithConfirmation()
                 .WithValidation(item => item == ConsoleColor.Black, "Choose black"));

            _test.Next(() => MenuTest());
        }

        private static void ListRawTest()
        {
            var colors = Enum.GetValues(typeof(ConsoleColor)).Cast<ConsoleColor>().ToList();
            var answer = _test.Prompt(Question.RawList("Choose favourite color", colors)
                 .WithDefaultValue(ConsoleColor.DarkCyan)
                 .WithConfirmation()
                 .WithValidation(item => item == ConsoleColor.Black, "Choose black"));

            _test.Next(() => MenuTest());
        }

        private static void ListCheckboxTest()
        {
            var colors = Enum.GetValues(typeof(ConsoleColor)).Cast<ConsoleColor>().ToList();
            var answer = _test.Prompt(Question.Checkbox("Choose favourite colors", colors)
                .WithDefaultValue(new List<ConsoleColor>() { ConsoleColor.Black, ConsoleColor.DarkGray })
                .WithConfirmation()
                .WithValidation(values => values.Any(item => item == ConsoleColor.Black), "Choose black"));

            _test.Next(() => MenuTest());
        }

        private static void ListExtendedTest()
        {
            var colors = new Dictionary<ConsoleKey, ConsoleColor>();
            colors.Add(ConsoleKey.B, ConsoleColor.Black);
            colors.Add(ConsoleKey.C, ConsoleColor.Cyan);
            colors.Add(ConsoleKey.D, ConsoleColor.DarkBlue);

            ConsoleColor answer = _test.Prompt(Question.ExtendedList("Choose favourite color", colors)
                .WithDefaultValue(ConsoleColor.Black)
                .WithConfirmation()
                .WithValidation(values => values == ConsoleColor.Black, "Choose black"));

            _test.Next(() => MenuTest());
        }
    }
}