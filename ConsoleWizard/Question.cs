using System;
using System.Linq;

namespace ConsoleWizard
{
    public static class Question
    {
        public static QuestionBase<string> TextInput(string message)
        {
            var inquire = new QuestionText<string>(message);
            inquire.ValidatationFn = v =>
            {
                if (string.IsNullOrEmpty(v) == false || inquire.HasDefaultValue)
                {
                    return true;
                }
                ConsoleHelper.WriteError("Empty line");
                return false;
            };

            inquire.ParseFn = v =>
            {
                return v;
            };

            return inquire;
        }

        public static QuestionBase<ConsoleKey> Confirm(string message)
        {
            var inquire = new QuestionKey<ConsoleKey>(message);
            inquire.Message += " [y/n]";
            inquire.ValidatationFn = v =>
            {
                if (v == ConsoleKey.Y || v == ConsoleKey.N)
                {
                    return true;
                }

                ConsoleHelper.WriteError("Press [[Y]] or [[N]]");
                return false;
            };

            inquire.ParseFn = v =>
            {
                return v;
            };

            return inquire;
        }

        public static QuestionBase<ConsoleKey> ConsoleKeyInput(string message, params ConsoleKey[] @params)
        {
            var inquire = new QuestionKey<ConsoleKey>(message);
            inquire.ValidatationFn = v =>
            {
                if (@params.Any(p => p == v))
                {
                    return true;
                }

                string keys = " Press : ";
                foreach (var key in @params)
                {
                    keys += $"[{(char)key}] ";
                }
                ConsoleHelper.WriteError(keys);
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