using System;
using System.Linq;

namespace ConsoleWizard
{
    public class Inquire
    {
        private string _question;

        public Inquire(string question)
        {
            _question = question;
        }

        public InquireBase<string> Input()
        {
            var inquire = new InquireText<string>(_question);
            inquire.ValidatationFn = v =>
            {
                if (string.IsNullOrEmpty(v) == false || inquire.HasDefaultValue)
                {
                    return true;
                }
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Empty line");
                Console.ResetColor();
                return false;
            };

            inquire.ParseFn = v =>
            {
                return v;
            };

            return inquire;
        }

        public InquireBase<ConsoleKey> ConsoleKey(params ConsoleKey[] @params)
        {
            var inquire = new InquireKey<ConsoleKey>(_question);
            inquire.ValidatationFn = v =>
            {
                if (@params.Any(p => p == v))
                {
                    return true;
                }
                Console.ForegroundColor = ConsoleColor.Red;
                string keys = " Press : ";
                foreach (var key in @params)
                {
                    keys += $"[{(char)key}] ";
                }

                Console.WriteLine(keys);
                Console.ResetColor();
                return false;
            };

            inquire.ParseFn = v =>
            {
                return v;
            };

            return inquire;
        }
    }
}