using System;
using System.Collections.Generic;
using System.Linq;
using InquirerCS;
using InquirerCS.Beta2;

namespace Sandbox2
{
    internal class Program
    {
        private static Inquirer _test = new Inquirer();

        private static void Main(string[] args)
        {
            _test.Next(() => MenuTest());
        }

        private static void MenuTest()
        {
            _test.Menu("Choose")
               .AddOption("InputTest", () => { InputTest(); })
               .AddOption("PasswordTest", () => { PasswordTest(); })
               .AddOption("ListTest", () => { ListTest(); })
               .AddOption("ListRawTest", () => { ListRawTest(); })
               .AddOption("ListCheckboxTest", () => { ListCheckboxTest(); })
               .AddOption("ListExtendedTest", () => { ListExtendedTest(); })
               .AddOption("ConfirmTest", () => { ConfirmTest(); }).Prompt();
        }

        private static void ConfirmTest()
        {
            Question.Confirm("Are you sure?")
                .WithConfirmation()
                .WithDefaultValue(false)
                .WithValidation(values => values == true, "You must be sure!").Prompt();
        }

        private static void InputTest()
        {
            Question.Input("How are you?")
                .WithDefaultValue("fine")
                .WithConfirmation()
                .WithValidation(value => value == "fine", "You cannot not be fine!").Prompt();
        }

        private static void ListCheckboxTest()
        {
            var colors = Enum.GetValues(typeof(ConsoleColor)).Cast<ConsoleColor>().ToList();
            Question.Checkbox("Choose favourite colors", colors)
                 .WithDefaultValue(new List<ConsoleColor>() { ConsoleColor.Black, ConsoleColor.DarkGray })
                 .WithConfirmation()
                 .WithValidation(values => values.Any(item => item == ConsoleColor.Black), "Choose black")
                 .ConvertToString(x => { return x + " Test"; }).Page(4).Prompt();
        }

        private static void ListExtendedTest()
        {
            var colors = new Dictionary<ConsoleKey, ConsoleColor>();
            colors.Add(ConsoleKey.B, ConsoleColor.Black);
            colors.Add(ConsoleKey.C, ConsoleColor.Cyan);
            colors.Add(ConsoleKey.D, ConsoleColor.DarkBlue);

            Question.ExtendedList("Choose favourite color", colors)
                .WithDefaultValue(ConsoleColor.Black)
                .WithConfirmation()
                .WithValidation(values => values == ConsoleColor.Black, "Choose black")
                .ConvertToString(value => value + " Test").Prompt();
        }

        private static void ListRawTest()
        {
            var colors = Enum.GetValues(typeof(ConsoleColor)).Cast<ConsoleColor>().ToList();
            Question.RawList("Choose favourite color", colors)
                 
                 .WithDefaultValue(ConsoleColor.DarkCyan)
                 .WithConfirmation()
                 .WithValidation(item => item == ConsoleColor.Black, "Choose black").Page(4).Prompt();
        }

        private static void ListTest()
        {
            var colors = Enum.GetValues(typeof(ConsoleColor)).Cast<ConsoleColor>().ToList();
            Question.List("Choose favourite color", colors)
                 .WithDefaultValue(ConsoleColor.DarkCyan)
                 .WithConfirmation()
                 .WithValidation(item => item == ConsoleColor.Black, "Choose black").Page(3).Prompt();
        }

      

        private static void PasswordTest()
        {
            Question.Password("Type password")
                .WithDefaultValue("123456789")
                .WithConfirmation()
                .WithValidation(value => value.Length > 8 && value.Length < 10, "Password length must be between 8-10 characters").Prompt();
        }
    }
}
