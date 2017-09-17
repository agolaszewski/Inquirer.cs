using ConsoleWizard;
using System;

namespace Demo2
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var wizard = new Wizard<Answers>();
            wizard.AddQuestion("1", new Inquire("1").Input(), a => a.One).Navigate(x =>
            {
                if (x == "Hello")
                {
                    return "2";
                }

                return null;
            });
            wizard.AddQuestion("2", new Inquire("2").Input(), a => a.Two).NavigateNext();
            wizard.AddQuestion("3", new Inquire("3").Input(), a => a.Three).NavigateNext();
            wizard.AddQuestion("4", new Inquire("4").Input(), a => a.Four);
            wizard.Run("1");
            Console.ReadKey();
        }
    }
}