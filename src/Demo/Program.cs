using System;
using System.Collections.Generic;
using System.Linq;
using InquirerCS;

namespace Demo
{
    public class ComplexClass
    {
        public string Name { get; set; }
        public ConsoleColor Color { get; set; }
    }

    internal class Program
    {
        private static List<ComplexClass> _complexClassList;

        private static void Main(string[] args)
        {
            _complexClassList = Enum.GetValues(typeof(ConsoleColor)).Cast<ConsoleColor>().Select(item => new ComplexClass()
            {
                Color = item,
                Name = item.ToString()
            }).ToList();

            Menu();
        }

        public static void Menu()
        {
            Question.Menu("Menu")
                .AddOption("Input", () => Input())
                .AddOption("InputInt", () => InputInt())
                .AddOption("Checkbox", () => Checkbox())
                .AddOption("CheckboxPaged", () => CheckboxPaged())
                .AddOption("Confirm", () => Confirm())
                .AddOption("Extended", () => Extended())
                .AddOption("ExtendedList", () => ExtendedList())
                .AddOption("List", () => List())
                .AddOption("PagedList", () => PagedList())
                .AddOption("RawList", () => PagedList())
                .AddOption("PagedRawList", () => PagedRawList())
                .AddOption("Password", () => Password())
                .AddOption("Exit", () => { })
                .Prompt();
        }

        public static void Input()
        {
            string result = Question.Input("How are you?")
                .WithConfirmation()
                .WithDefaultValue("fine")
                .WithValidation(answer => answer == "fine", "You must be fine!")
                .Prompt();

            Menu();
        }

        public static void InputInt()
        {
            int result = Question.Input<int>("2 + 2 =")
                .WithConfirmation()
                .WithDefaultValue(4)
                .WithValidation(answer => answer == 4, "Try again")
                .Prompt();

            Menu();
        }

        public static void Checkbox()
        {
            List<ConsoleColor> defaults = new List<ConsoleColor>() { ConsoleColor.Cyan, ConsoleColor.Yellow, ConsoleColor.DarkCyan };

            var result = Question.Checkbox("Colors", _complexClassList)
                .WithConvertToString(item => item.Name)
                .WithDefaultValue(item => defaults.Any(d => d == item.Color))
                .WithConfirmation()
                .WithValidation(answers => answers.Any(x => x.Color == ConsoleColor.Black), "Pick black")
                .Prompt();

            Menu();
        }

        public static void CheckboxPaged()
        {
            List<ConsoleColor> defaults = new List<ConsoleColor>() { ConsoleColor.Cyan, ConsoleColor.Yellow, ConsoleColor.DarkCyan };

            var result = Question.Checkbox("Colors", _complexClassList)
                .Page(3)
                .WithConvertToString(item => item.Name)
                .WithDefaultValue(item => defaults.Any(d => d == item.Color))
                .WithConfirmation()
                .WithValidation(answers => answers.Any(x => x.Color == ConsoleColor.Black), "Pick black")
                .Prompt();

            Menu();
        }

        public static void Confirm()
        {
            bool result = Question.Confirm("Are you sure?")
                .WithConfirmation()
                .WithDefaultValue(true)
                .WithValidation(answer => answer == true, "You must be sure!")
                .Prompt();

            Menu();
        }

        public static void Extended()
        {
            ConsoleKey result = Question.Extended("[Yes] or [N]o", ConsoleKey.Y, ConsoleKey.N)
                .WithConfirmation()
                .WithDefaultValue(ConsoleKey.Y)
                .WithValidation(answer => answer == ConsoleKey.Y, "Yes!!!")
                .Prompt();

            Menu();
        }

        public static void ExtendedList()
        {
            ConsoleColor result = Question.ExtendedList("Pick",
                    _complexClassList
                    .GroupBy(x => x.Name[0])
                    .Select(x => x.First())
                    .ToDictionary(key => (ConsoleKey)((int)key.Name[0]), item => item.Color))
                .WithConfirmation()
                .WithDefaultValue(ConsoleColor.Black)
                .WithValidation(answer => answer == ConsoleColor.Black, "Pick black")
                .Prompt();

            Menu();
        }

        public static void List()
        {
            ComplexClass result = Question.List("Pick", _complexClassList)
                .WithConfirmation()
                .WithConvertToString(x => x.Name)
                .WithDefaultValue(x => x.Color == ConsoleColor.Black)
                .WithValidation(answer => answer.Color == ConsoleColor.Black, "Pick black")
                .Prompt();

            Menu();
        }

        public static void PagedList()
        {
            ComplexClass result = Question.List("Pick", _complexClassList)
                .WithConfirmation()
                .WithConvertToString(x => x.Name)
                .WithDefaultValue(x => x.Color == ConsoleColor.Black)
                .WithValidation(answer => answer.Color == ConsoleColor.Black, "Pick black")
                .Page(4)
                .Prompt();

            Menu();
        }

        public static void RawList()
        {
            ComplexClass result = Question.RawList("Pick", _complexClassList)
                .WithConfirmation()
                .WithConvertToString(x => x.Name)
                .WithDefaultValue(x => x.Color == ConsoleColor.Black)
                .WithValidation(answer => answer.Color == ConsoleColor.Black, "Pick black")
                .Prompt();

            Menu();
        }

        private static void PagedRawList()
        {
            ComplexClass result = Question.RawList("Pick", _complexClassList)
                .WithConfirmation()
                .WithConvertToString(x => x.Name)
                .WithDefaultValue(x => x.Color == ConsoleColor.Black)
                .WithValidation(answer => answer.Color == ConsoleColor.Black, "Pick black")
                .Page(3)
                .Prompt();

            Menu();
        }

        private static void Password()
        {
            string result = Question.Password("Top secret password?")
               .WithConfirmation()
               .Prompt();

            Menu();
        }
    }
}