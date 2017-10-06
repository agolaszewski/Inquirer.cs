using ConsoleWizard;
using System;
using System.Collections.Generic;
using System.Linq;

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
            //ConfirmTest();
            //PasswordTest();
            //ListTest();
            //ListRawTest();
            //ListCheckboxTest();
            ListExtendedTest();
            Console.WriteLine(_test.Answers.Input);
            Console.ReadKey();
        }

        private static void InputTest()
        {
            _test.For(x => x.Input).Prompt(Question.Input("How are you?").WithDefaultValue("fine"));
        }

        private static void InputTestNumber()
        {
            _test.For(x => x.InputNumber).Prompt(Question.Input<int>("2+2").WithDefaultValue(4));
        }

        private static void ConfirmTest()
        {
            _test.For(x => x.One).Prompt(Question.Confirm("Are you sure?").WithDefaultValue(ConsoleKey.Y));
        }

        private static void PasswordTest()
        {
            _test.For(x => x.Input).Prompt(Question.Password("How are you?").WithDefaultValue("123456"));
        }

     
        private static void ListTest()
        {
            var list = Enum.GetValues(typeof(ConsoleColor)).Cast<ConsoleColor>().ToList();
            _test.For(x => x.Two).Prompt(Question.List("Choose color?", list).WithDefaultValue(ConsoleColor.DarkYellow));
        }

        private static void ListRawTest()
        {
            var list = Enum.GetValues(typeof(ConsoleColor)).Cast<ConsoleColor>().ToList();
            _test.For(x => x.Two).Prompt(Question.RawList("Choose color?", list).WithDefaultValue(ConsoleColor.DarkRed));
        }

        private static void ListCheckboxTest()
        {
            var list = Enum.GetValues(typeof(ConsoleColor)).Cast<ConsoleColor>().ToList();
            _test.For(x => x.Colors).Prompt(Question.Checkbox("Chose favourite colors", list).WithDefaultValue(ConsoleColor.DarkBlue));
        }

        private static void ListExtendedTest()
        {
            var list = new Dictionary<ConsoleKey, ConsoleColor>();
            list.Add(ConsoleKey.B, ConsoleColor.Blue);
            list.Add(ConsoleKey.C, ConsoleColor.Cyan);
            list.Add(ConsoleKey.D, ConsoleColor.DarkBlue);

            _test.For(x => x.Two).Prompt(Question.ExtendedList("sdada", list).WithDefaultValue(ConsoleColor.DarkBlue));
        }
    }
}