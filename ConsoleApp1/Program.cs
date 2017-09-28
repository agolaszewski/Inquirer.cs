using ConsoleWizard;
using System;
using System.Collections.Generic;

namespace ConsoleApp1
{
    internal class Program
    {
        private static Inquirer<Answers> _test;

        private static void Main(string[] args)
        {
            _test = new Inquirer<Answers>();
            Herp();
        }

        private static void Hur()
        {
            _test.For(x => x.One).Prompt(Question.ConsoleKeyInput("[[Z]] or [[A]]", ConsoleKey.Z, ConsoleKey.A)).Then(answers =>
            {
                Herp();
            });
        }

        private static void Herp()
        {
            var list = new List<ConsoleColor>() { ConsoleColor.Cyan, ConsoleColor.DarkGray, ConsoleColor.DarkRed, ConsoleColor.Yellow };
            _test.For(x => x.Two).Prompt(Question.List<ConsoleColor>("Chose color", list)).Then
        }
    }
}