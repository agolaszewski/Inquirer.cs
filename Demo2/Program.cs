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
            wizard.AddQuestion("1", xxx, a => a.Derp );
            wizard.AddQuestion("2", xxx, a => a.Derp);
            wizard.AddQuestion("3", xxx, a => a.Derp);
            wizard.Run("1");
            Console.WriteLine(xyz);
            Console.ReadKey();
        }
    }
}