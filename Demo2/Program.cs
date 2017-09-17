using ConsoleWizard;
using System;

namespace Demo2
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var xxx = new Inquire("asdas").Input().WithOptions.WithDefaultValue("Derp").WithConfirmation();
            new Inquire("asdasda").ConsoleKey().WithOptions.WithDefaultValue(ConsoleKey.A);

            string xyz = "";
            var wizard = new Wizard<Answers>();
            wizard.AddQuestion("1", new Inquire("1").Input(), a => a.One).NavigateNext();
            wizard.AddQuestion("2", new Inquire("2").Input(), a => a.Two).NavigateNext();
            wizard.AddQuestion("3", new Inquire("3").Input(), a => a.Three).NavigateNext();
            wizard.AddQuestion("4", new Inquire("4").Input(), a => a.Four);
            wizard.Run("1");
            Console.WriteLine(xyz);
            Console.ReadKey();
        }
    }
}