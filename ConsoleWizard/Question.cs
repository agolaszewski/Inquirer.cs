using System;
using System.Collections.Generic;
using System.Linq;

namespace ConsoleWizard
{
    public static class Question
    {
        public static QuestionCheckbox<List<T>, T> Checkbox<T>(string message, List<T> choices)
        {
            var inquire = new QuestionCheckbox<List<T>, T>(message);
            inquire.Choices = choices;
            inquire.Selected = new bool[choices.Count];

            inquire.DisplayQuestionAnswersFn = (index, choice) =>
            {
                return $"{choice}";
            };

            inquire.ParseFn = v =>
            {
                return inquire.Choices[v - 1];
            };

            inquire.ToStringFn = v => { return string.Join(",", v); };

            return inquire;
        }

        public static QuestionBase<ConsoleKey> Confirm(string message)
        {
            var inquire = new QuestionInputKey<ConsoleKey>(message);
            inquire.Message += " [y/n]";
            inquire.ValidatationFn = v =>
            {
                if (v == System.ConsoleKey.Y || v == System.ConsoleKey.N)
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

        public static QuestionBase<ConsoleKey> Extended(string message, params ConsoleKey[] @params)
        {
            var inquire = new QuestionInputKey<ConsoleKey>(message);
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

        public static QuestionExtendedList<Dictionary<ConsoleKey, T>, T> ExtendedList<T>(string message, Dictionary<ConsoleKey, T> choices)
        {
            var inquire = new QuestionExtendedList<Dictionary<ConsoleKey, T>, T>(message);
            inquire.Choices = choices;

            inquire.ValidatationFn = v =>
            {
                if (inquire.Choices.ContainsKey(v))
                {
                    return true;
                }

                ConsoleHelper.WriteError($"Invalid key");
                return false;
            };

            inquire.DisplayQuestionAnswersFn = (index, choice) =>
            {
                return $"[{index}] {choice}";
            };

            inquire.ParseFn = v =>
            {
                return inquire.Choices[v];
            };

            return inquire;
        }

        public static QuestionBase<T> Input<T>(string message) where T : struct
        {
            var inquire = new QuestionInput<T>(message);
            inquire.ValidatationFn = v =>
            {
                if (string.IsNullOrEmpty(v) == false || inquire.HasDefaultValue)
                {
                    if (v.ToN<T>().HasValue)
                    {
                        return true;
                    }
                    else
                    {
                        ConsoleHelper.WriteError($"Cannot parse {v} to {typeof(T)}");
                        return false;
                    }
                }
                else
                {
                    ConsoleHelper.WriteError("Empty line");
                    return false;
                }
            };

            inquire.ParseFn = v =>
            {
                return v.To<T>();
            };

            return inquire;
        }

        public static QuestionBase<string> Input(string message)
        {
            var inquire = new QuestionInput<string>(message);
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

        public static QuestionList<T> List<T>(string message, List<T> choices)
        {
            var inquire = new QuestionList<T>(message);
            inquire.Choices = choices;

            inquire.DisplayQuestionAnswersFn = (index, choice) =>
            {
                return $"{choice}";
            };

            inquire.ParseFn = v =>
            {
                return inquire.Choices[v - 1];
            };

            return inquire;
        }

        public static QuestionBase<string> Password(string message)
        {
            var inquire = new QuestionPassword<string>(message);
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

        public static QuestionRawList<T> RawList<T>(string message, List<T> choices)
        {
            var inquire = new QuestionRawList<T>(message);
            inquire.Choices = choices;

            inquire.DisplayQuestionAnswersFn = (index, choice) =>
            {
                return $"[{index}] {choice}";
            };

            inquire.ValidatationFn = v =>
            {
                if (v > 0 && v <= inquire.Choices.Count)
                {
                    return true;
                }

                ConsoleHelper.WriteError($"Choosen number must be between 1 and {inquire.Choices.Count}");
                return false;
            };

            inquire.ParseFn = v =>
            {
                return inquire.Choices[v - 1];
            };

            return inquire;
        }
    }
}
