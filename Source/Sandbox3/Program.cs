using System;
using System.Collections.Generic;
using System.Linq;
using InquirerCS;
using InquirerCS.Traits;

namespace Sandbox2
{
    internal class Program
    {
        private static Inquirer _test = new Inquirer();

        private static void ConfirmTest()
        {
            Question.Confirm("Are you sure?")
                .WithConfirmation()
                .WithDefaultValue(false)
                .WithValidation(values => values == true, "You must be sure!")
                .Build().Prompt();
        }

        private static void ExtendedTest()
        {
            var colors = Enum.GetValues(typeof(ConsoleColor)).Cast<ConsoleColor>().ToList();
            Question.Extended("[Y]es or [N] or [M]aybe", ConsoleKey.Y, ConsoleKey.N, ConsoleKey.M)
                 .WithDefaultValue(ConsoleKey.N)
                 .WithConfirmation()
                 .WithValidation(values => values != ConsoleKey.M, "You must be sure").Build().Prompt();
        }

        private static void InputTest()
        {
            Question.Input("How are you?")
                .WithDefaultValue("fine")
                .WithConfirmation()
                .WithValidation(value => value == "fine", "You cannot be not fine!")
                .Build().Prompt();
        }

        private static void ListCheckboxTest()
        {
            var colors = Enum.GetValues(typeof(ConsoleKey)).Cast<ConsoleKey>().ToList();
            Question.Checkbox(string.Join(" ", Enum.GetValues(typeof(ConsoleKey)).Cast<ConsoleKey>()), colors)
                 .Page(11)
                 .WithDefaultValue(colors)
                 .WithConfirmation()
                 .WithValidation(values => values.Any(item => item == ConsoleKey.A), "Choose black")
                 .WithConvertToString(x => { return x + " Test"; })
                 .Build().Prompt();
        }

        private static void ListExtendedTest()
        {
            var colors = new Dictionary<ConsoleKey, ConsoleColor>();
            colors.Add(ConsoleKey.B, ConsoleColor.Black);
            colors.Add(ConsoleKey.C, ConsoleColor.Cyan);
            colors.Add(ConsoleKey.D, ConsoleColor.DarkBlue);
        }

        private static void ListRawTest()
        {
            var colors = Enum.GetValues(typeof(ConsoleColor)).Cast<ConsoleColor>().ToList();
            Question.RawList("Choose favourite color", colors)
                 .WithDefaultValue(ConsoleColor.DarkCyan)
                 .WithConfirmation()
                 //.Page(10)
                 .WithValidation(item => item == ConsoleColor.Black, "Choose black").Build().Prompt();
        }

        private static void ListTest()
        {
            var colors = Enum.GetValues(typeof(ConsoleColor)).Cast<ConsoleColor>().ToList();
            Question.List("Choose favourite color", colors)
                 .WithDefaultValue(ConsoleColor.DarkCyan)
                 .WithConfirmation()
                 .WithValidation(item => item == ConsoleColor.Black, "Choose black")
                 .Build().Prompt();
        }

        private static void Main(string[] args)
        {
            ListCheckboxTest();
        }

        private static void PasswordTest()
        {
            Question.Password("Type password")
                .WithDefaultValue("123456789")
                .WithConfirmation()
                .WithValidation(value => value.Length > 8 && value.Length < 10, "Password length must be between 8-10 characters")
                .Build().Prompt();
        }
    }
}
