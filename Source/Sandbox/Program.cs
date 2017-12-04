using System;
using InquirerCS;

namespace ConsoleApp1
{
    internal class Program
    {
        private static Inquirer _test = new Inquirer();
        private static Answers _answers = new Answers();

        private static void Main(string[] args)
        {
            //_test.Next(() => MenuTest());
            Console.ReadKey();
        }

        //private static void MenuTest()
        //{
        //    _test.Menu("Choose")
        //       .AddOption("PagingCheckboxTest", () => { PagingCheckboxTest(); })
        //       .AddOption("PagingRawListTest", () => { PagingRawListTest(); })
        //       .AddOption("PagingListTest", () => { PagingListTest(); })
        //       .AddOption("InputTest", () => { InputTest(); })
        //       .AddOption("PasswordTest", () => { PasswordTest(); })
        //       .AddOption("ListTest", () => { ListTest(); })
        //       .AddOption("ListRawTest", () => { ListRawTest(); })
        //       .AddOption("ListCheckboxTest", () => { ListCheckboxTest(); })
        //       .AddOption("ListExtendedTest", () => { ListExtendedTest(); })
        //       .AddOption("ConfirmTest", () => { ConfirmTest(); }).Prompt();
        //}

        //private static void PagingCheckboxTest()
        //{
        //    var colors = Enum.GetValues(typeof(ConsoleColor)).Cast<ConsoleColor>().ToList();
        //    var answer = _test.Prompt(Question.Checkbox("Choose favourite colors", colors)
        //        .Page(3)
        //        .WithDefaultValue(new List<ConsoleColor>() { ConsoleColor.Black, ConsoleColor.DarkGray })
        //        .WithConfirmation()
        //        .WithValidation(values => values.Any(item => item == ConsoleColor.Black), "Choose black"));

        //    _test.Next(() => MenuTest());
        //}

        //private static void PagingRawListTest()
        //{
        //    var colors = Enum.GetValues(typeof(ConsoleColor)).Cast<ConsoleColor>().ToList();
        //    var answer = _test.Prompt(Question.RawList("Choose favourite color", colors)
        //        .Page(3)
        //        .WithDefaultValue(ConsoleColor.DarkCyan)
        //        .WithConfirmation()
        //        .WithValidation(item => item == ConsoleColor.Black, "Choose black"));
        //    _test.Next(() => MenuTest());
        //}

        //private static void PagingListTest()
        //{
        //    var colors = Enum.GetValues(typeof(ConsoleColor)).Cast<ConsoleColor>().ToList();
        //    var answer = _test.Prompt(Question.List("Choose favourite color", colors)
        //        .Page(3)
        //        .WithDefaultValue(ConsoleColor.DarkCyan)
        //        .WithConfirmation()
        //        .WithValidation(item => item == ConsoleColor.Black, "Choose black"));

        //    _test.Next(() => MenuTest());
        //}

        //private static void ListTest()
        //{
        //    var colors = Enum.GetValues(typeof(ConsoleColor)).Cast<ConsoleColor>().ToList();
        //    var answer = _test.Prompt(Question.List("Choose favourite color", colors)
        //         .WithDefaultValue(ConsoleColor.DarkCyan)
        //         .WithConfirmation()
        //         .WithValidation(item => item == ConsoleColor.Black, "Choose black"));

        //    _test.Next(() => MenuTest());
        //}

        //private static void ListRawTest()
        //{
        //    var colors = Enum.GetValues(typeof(ConsoleColor)).Cast<ConsoleColor>().ToList();
        //    var answer = _test.Prompt(Question.RawList("Choose favourite color", colors)
        //         .WithDefaultValue(ConsoleColor.DarkCyan)
        //         .WithConfirmation()
        //         .WithValidation(item => item == ConsoleColor.Black, "Choose black"));

        //    _test.Next(() => MenuTest());
        //}

        //private static void ListCheckboxTest()
        //{
        //    var colors = Enum.GetValues(typeof(ConsoleColor)).Cast<ConsoleColor>().ToList();
        //    var answer = _test.Prompt(Question.Checkbox("Choose favourite colors", colors)
        //        .WithDefaultValue(new List<ConsoleColor>() { ConsoleColor.Black, ConsoleColor.DarkGray })
        //        .WithConfirmation()
        //        .WithValidation(values => values.Any(item => item == ConsoleColor.Black), "Choose black"));

        //    _test.Next(() => MenuTest());
        //}

        //private static void ListExtendedTest()
        //{
        //    var colors = new Dictionary<ConsoleKey, ConsoleColor>();
        //    colors.Add(ConsoleKey.B, ConsoleColor.Black);
        //    colors.Add(ConsoleKey.C, ConsoleColor.Cyan);
        //    colors.Add(ConsoleKey.D, ConsoleColor.DarkBlue);

        //    ConsoleColor answer = _test.Prompt(Question.ExtendedList("Choose favourite color", colors)
        //        .WithDefaultValue(ConsoleColor.Black)
        //        .WithConfirmation()
        //        .WithValidation(values => values == ConsoleColor.Black, "Choose black"));

        //    _test.Next(() => MenuTest());
        //}
    }
}