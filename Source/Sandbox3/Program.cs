using System;
using System.Collections.Generic;
using System.Linq;
using InquirerCS;
using InquirerCS.Builders;

namespace Sandbox2
{
    internal class Program
    {
        private static Inquirer _test = new Inquirer();

        private static void ConfirmTest()
        {
            var colors = Enum.GetValues(typeof(ConsoleColor)).Cast<ConsoleColor>().ToList();

            _test.Prompt(Question.Confirm(string.Join(" ", Enum.GetValues(typeof(ConsoleKey)).Cast<ConsoleKey>()))
                .WithConfirmation()
                .WithDefaultValue(false)
                .WithValidation(values => values == true, "You must be sure!"));

            //_test.Next(() => ExtendedTest());
        }

        //private static void ExtendedTest()
        //{
        //    var colors = Enum.GetValues(typeof(ConsoleColor)).Cast<ConsoleColor>().ToList();
        //    _test.Prompt(Question.Extended(string.Join(" ", Enum.GetValues(typeof(ConsoleKey)).Cast<ConsoleKey>()), ConsoleKey.Y, ConsoleKey.N, ConsoleKey.M)
        //         .WithDefaultValue(ConsoleKey.N)
        //         .WithConfirmation()
        //         .WithValidation(values => values != ConsoleKey.M, "You must be sure"));
        //}

        private static void InputTest()
        {
            var colors = Enum.GetValues(typeof(ConsoleColor)).Cast<ConsoleColor>().ToList();

            Question.Input(string.Join(" ", Enum.GetValues(typeof(ConsoleKey)).Cast<ConsoleKey>()))
                .WithDefaultValue(string.Join(" ", Enum.GetValues(typeof(ConsoleKey)).Cast<ConsoleKey>()))
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

        //private static void ListExtendedTest()
        //{
        //    var colors = new Dictionary<ConsoleKey, ConsoleColor>();
        //    colors.Add(ConsoleKey.B, ConsoleColor.Black);
        //    colors.Add(ConsoleKey.C, ConsoleColor.Cyan);
        //    colors.Add(ConsoleKey.D, ConsoleColor.DarkBlue);
        //}

        //private static void ListRawTest()
        //{
        //    var colors = Enum.GetValues(typeof(ConsoleColor)).Cast<ConsoleColor>().ToList();
        //    Question.RawList(string.Join(" ", Enum.GetValues(typeof(ConsoleKey)).Cast<ConsoleKey>()), colors)
        //         .WithDefaultValue(ConsoleColor.DarkCyan)
        //         .WithConfirmation()
        //         .WithConvertToString(x => { return x + " Test"; })
        //         .Page(10)
        //         .WithValidation(item => item == ConsoleColor.Black, "Choose black").Build().Prompt();
        //}

        //private static void ListTest()
        //{
        //    Question.List<ConsoleKey>("Asdasd", new System.Collections.Generic.List<ConsoleKey> { ConsoleKey.L }).WithDefaultValue(null);
        //    //var colors = Enum.GetValues(typeof(ConsoleColor)).Cast<ConsoleColor>().ToList();
        //    //Question.List(string.Join(" ", Enum.GetValues(typeof(ConsoleKey)).Cast<ConsoleKey>()), colors)
        //    //    .WithConfirmation()
        //    //     .WithDefaultValue(ConsoleColor.DarkCyan)
        //    //     .WithConfirmation()
        //    //     .WithConvertToString(x => { return x + " Test"; })
        //    //     .Page(4)
        //    //     //.WithValidation(item => item == ConsoleColor.Black, "Choose black")
        //    //     .Build().Prompt();
        //}

        private static void Main(string[] args)
        {
            Question.Password("ASdasd");
        }

        //private static void PasswordTest()
        //{
        //    var colors = Enum.GetValues(typeof(ConsoleColor)).Cast<ConsoleColor>().ToList();

        //    Question.Password(string.Join(" ", Enum.GetValues(typeof(ConsoleKey)).Cast<ConsoleKey>()))
        //        .WithDefaultValue("123456789")
        //        .WithConfirmation()
        //        .WithValidation(value => value.Length > 8 && value.Length < 10, "Password length must be between 8-10 characters")
        //        .Build().Prompt();
        //}
    }
}
