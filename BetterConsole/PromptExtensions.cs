using System;
using System.Linq;

namespace BetterConsole
{
    public static class PromptExtensions
    {
        public static bool Confirm(this Prompt<bool> prompt)
        {
            prompt._message += " [y/n] ";
            prompt._readModeFn = () => { return Console.ReadKey().KeyChar.ToString(); };
            prompt._validatorFn = v =>
            {
                if (string.IsNullOrEmpty(v) == false && (v.ToUpper()[0] == (char)System.ConsoleKey.N || v.ToUpper()[0] == (char)System.ConsoleKey.Y))
                {
                    return true;
                }
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($" [[Y]es or [N]o");
                Console.ResetColor();
                return false;
            };

            prompt._parseFn = v =>
            {
                return (v.ToUpper()[0] == (char)System.ConsoleKey.Y);
            };

            return prompt.Run();
        }

        public static string Input(this Prompt<string> prompt)
        {
            prompt._validatorFn = v =>
            {
                if (string.IsNullOrEmpty(v) == false || prompt._hasDefaultValue)
                {
                    return true;
                }
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"Empty line");
                Console.ResetColor();
                return false;
            };

            prompt._parseFn = v =>
            {
                return v;
            };

            return prompt.Run();
        }

        public static ConsoleKey ConsoleKey(this Prompt<ConsoleKey> prompt, params ConsoleKey[] @params)
        {
            prompt._readModeFn = () => { return Console.ReadKey().KeyChar.ToString(); };
            prompt._validatorFn = v =>
            {
                var consoleKey = (ConsoleKey)((int)char.ToUpper(v[0]));

                if (@params.Any(p => p == consoleKey))
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

            prompt._parseFn = v =>
            {
                return (ConsoleKey)((int)char.ToUpper(v[0]));
            };

            return prompt.Run();
        }
    }
}