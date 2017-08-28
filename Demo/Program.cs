using BetterConsole;
using System;

namespace Demo
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var q1 = new Inquire("What is your name?").Input().WithDefault("Andrew");
            var q2 = new Inquire("What is your surname?").Input().WithDefault("Andrew");
            var q3 = new Inquire("Are you [M]ale or [F]emale").ConsoleKey(ConsoleKey.M, ConsoleKey.F);
            var q4 = new Inquire("Do you want to stop?").Confirm().WithDefault(false);

            q1.Navigate(q2);
            q2.Navigate(q3);
            q3.Navigate(q4);
            q4.Navigate(x => {
                if(!x)
                {
                    q1.Prompt();
                }
            });

            //new Inquire("Are you from Poland?").Confirm().Prompt();
            //new Inquire("Choose from one to five").SingleChoice<int>(Enumerable.Range(1, 5).ToList()).Prompt();
            q1.Prompt();
        }
    }
}