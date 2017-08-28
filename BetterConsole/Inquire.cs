using System;
using System.Collections.Generic;
using System.Linq;

namespace BetterConsole
{
    public class Inquire
    {
        public string Question { get; private set; }

        public Inquire(string question)
        {
            Question = question;
        }

        public InquireBase<bool> Confirm()
        {
            var betterPrompt = new InquireKey<bool>();
            betterPrompt.Question = Question + " [y/n]";

            betterPrompt.ValidatationFn = v =>
            {
                if (v == System.ConsoleKey.N || v == System.ConsoleKey.Y)
                {
                    return true;
                }
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($" [[Y]es or [N]o");
                Console.ResetColor();
                return false;
            };

            betterPrompt.ParseFn = v =>
            {
                return (v == System.ConsoleKey.Y);
            };

            return betterPrompt;
        }

        public InquireBase<string> Input()
        {
            var betterPrompt = new InquireText<string>();
            betterPrompt.Question = Question;

            betterPrompt.ValidatationFn = v =>
            {
                if (string.IsNullOrEmpty(v) == false || betterPrompt.HasDefaultValue)
                {
                    return true;
                }
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"Empty line");
                Console.ResetColor();
                return false;
            };

            betterPrompt.ParseFn = v =>
            {
                return v;
            };

            return betterPrompt;
        }

        public InquireBase<ConsoleKey> ConsoleKey(params ConsoleKey[] @params)
        {
            var betterPrompt = new InquireKey<ConsoleKey>();
            betterPrompt.Question = Question;

            betterPrompt.ValidatationFn = v =>
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

            betterPrompt.ParseFn = v =>
            {
                return v;
            };

            return betterPrompt;
        }

        public InquireMultiAnswersBase<T> SingleChoice<T>(List<T> choices, Func<T, string> choiceToStringFn = null)
        {
            var betterPrompt = new InquireSingleChoice<T>();
            betterPrompt.Choices = choices;
            betterPrompt.Question = Question;

            Func<T, string> defaultChoiceToStringFn = v => { return v.ToString(); };
            betterPrompt.ChoiceToStringFn = choiceToStringFn ?? defaultChoiceToStringFn;

            betterPrompt.ValidatationFn = v =>
            {
                var value = v.ToN<int>();
                if (value.HasValue)
                {
                    if (value > 0 && value <= betterPrompt.Choices.Count)
                    {
                        return true;
                    }
                }
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"Choose value from 1 to {betterPrompt.Choices.Count}");
                Console.ResetColor();
                return false;
            };

            betterPrompt.ParseFn = v =>
            {
                var index = v.To<int>();
                return choices[index - 1];
            };

            return betterPrompt;
        }
    }
}