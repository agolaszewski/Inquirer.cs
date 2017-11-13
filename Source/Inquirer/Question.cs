using System;
using System.Collections.Generic;
using System.Linq;

namespace InquirerCS
{
    public static class Question
    {
        public static QuestionCheckbox<List<TResult>, TResult> Checkbox<TResult>(string message, List<TResult> choices) 
        {
            var inquire = new QuestionCheckbox<List<TResult>, TResult>(message);
            inquire.Choices = choices;

            inquire.ParseFn = answer =>
            {
                return inquire.Choices[answer - 1];
            };

            inquire.ConvertToStringFn = answer => { return string.Join(",", answer); };

            return inquire;
        }

        public static QuestionInputKey<bool> Confirm(string message)
        {
            var inquire = new QuestionInputKey<bool>(message);
            inquire.Message += " [y/n]";
            inquire.ValidatationFn = answer =>
            {
                if (answer == System.ConsoleKey.Y || answer == System.ConsoleKey.N)
                {
                    return true;
                }

                ConsoleHelper.WriteError("Press [[Y]] or [[N]]");
                return false;
            };

            inquire.ParseFn = answer =>
            {
                return answer == System.ConsoleKey.Y;
            };

            return inquire;
        }

        public static QuestionInputKey<ConsoleKey> Extended(string message, params ConsoleKey[] @params)
        {
            var inquire = new QuestionInputKey<ConsoleKey>(message);
            inquire.ValidatationFn = answer =>
            {
                if (@params.Any(p => p == answer))
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

            inquire.ParseFn = answer =>
            {
                return answer;
            };

            return inquire;
        }

        public static QuestionExtendedList<Dictionary<ConsoleKey, TResult>, TResult> ExtendedList<TResult>(string message, Dictionary<ConsoleKey, TResult> choices)
        {
            var inquire = new QuestionExtendedList<Dictionary<ConsoleKey, TResult>, TResult>(message);
            inquire.Choices = choices;

            inquire.ValidatationFn = answer =>
            {
                if (inquire.Choices.ContainsKey(answer))
                {
                    return true;
                }

                ConsoleHelper.WriteError($"Invalid key");
                return false;
            };

            inquire.ParseFn = answer =>
            {
                return inquire.Choices[answer];
            };

            return inquire;
        }

        public static QuestionInput<T> Input<T>(string message) where T : struct
        {
            var inquire = new QuestionInput<T>(message);
            //inquire.ValidatationFn = answer =>
            //{
            //    if (string.IsNullOrEmpty(answer) == false || inquire.HasDefaultValue)
            //    {
            //        if (answer.ToN<T>().HasValue)
            //        {
            //            return true;
            //        }
            //        else
            //        {
            //            ConsoleHelper.WriteError($"Cannot parse {answer} to {typeof(T)}");
            //            return false;
            //        }
            //    }
            //    else
            //    {
            //        ConsoleHelper.WriteError("Empty line");
            //        return false;
            //    }
            //};

            inquire.ParseFn = answer =>
            {
                return answer.To<T>();
            };

            return inquire;
        }

        public static QuestionInput<string> Input(string message)
        {
            var inquire = new QuestionInput<string>(message);
            inquire.ValidatationFn = answer =>
            {
                if (string.IsNullOrEmpty(answer) == false || inquire.HasDefaultValue)
                {
                    return true;
                }

                ConsoleHelper.WriteError("Empty line");
                return false;
            };

            inquire.ParseFn = answer =>
            {
                return answer;
            };

            return inquire;
        }

        public static QuestionList<T> List<T>(string message, List<T> choices)
        {
            var inquire = new QuestionList<T>(message);
            inquire.Choices = choices;

            inquire.ParseFn = answer =>
            {
                return inquire.Choices[answer - 1];
            };

            return inquire;
        }

        public static QuestionPassword<string> Password(string message)
        {
            var inquire = new QuestionPassword<string>(message);
            inquire.ValidatationFn = answer =>
            {
                if (string.IsNullOrEmpty(answer) == false || inquire.HasDefaultValue)
                {
                    return true;
                }

                ConsoleHelper.WriteError("Empty line");
                return false;
            };

            inquire.ParseFn = answer =>
            {
                return answer;
            };

            return inquire;
        }

        public static QuestionRawList<T> RawList<T>(string message, List<T> choices) 
        {
            var inquire = new QuestionRawList<T>(message);
            inquire.Choices = choices;

            inquire.ValidatationFn = answer =>
            {
                if (answer > 0 && answer <= inquire.Choices.Count)
                {
                    return true;
                }

                ConsoleHelper.WriteError($"Choosen number must be between 1 and {inquire.Choices.Count}");
                return false;
            };

            inquire.ParseFn = answer =>
            {
                return inquire.Choices[answer - 1];
            };

            return inquire;
        }
    }
}