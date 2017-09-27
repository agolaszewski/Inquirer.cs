using System;
using System.Collections.Generic;
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

        public static QuestionBase<long> IntegerInput(string message)
        {
            var inquire = new QuestionText<long>(message);
            inquire.ValidatationFn = v =>
            {
                if (v.ToN<long>().HasValue)
                {
                    return true;
                }
                ConsoleHelper.WriteError("Not a integer");
                return false;
            };

            inquire.ParseFn = v =>
            {
                return v.To<long>();
            };

            return inquire;
        }

        public static QuestionBase<T> List<T>(string message, List<T> choices)
        {
            var inquire = new QuestionList<T>(message);
            inquire.Choices = choices;

            inquire.DisplayQuestionAnswersFn = (index, choice) =>
            {
                Console.WriteLine($"[{index}] {choice}");
            };

            inquire.ValidatationFn = v =>
            {
                if (v > 0 && v <= inquire.Choices.Count)
                {
                    return true;
                }
                return false;
            };

            inquire.ParseFn = v =>
            {
                return inquire.Choices[v - 1];
            };

            inquire.ReadFn = () =>
            {
                return Console.ReadLine().ToN<int>();
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