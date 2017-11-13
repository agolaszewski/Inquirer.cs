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

            inquire.WithInputValidation(value => { return value == ConsoleKey.A ? true : false; }, "Press [[Y]] or [[N]]");

            inquire.Parse(answer =>
            {
                return answer == System.ConsoleKey.Y;
            });

            return inquire;
        }

        public static QuestionInputKey<ConsoleKey> Extended(string message, params ConsoleKey[] @params)
        {
            var inquire = new QuestionInputKey<ConsoleKey>(message);

            inquire.WithInputValidation(
            value =>
            {
                return @params.Any(p => p == value);
            },
            value =>
            {
                string keys = " Press : ";
                foreach (var key in @params)
                {
                    keys += $"[{(char)key}] ";
                }

                return keys;
            });

            inquire.Parse(answer =>
            {
                return answer;
            });

            return inquire;
        }

        public static QuestionExtendedList<Dictionary<ConsoleKey, TResult>, TResult> ExtendedList<TResult>(string message, Dictionary<ConsoleKey, TResult> choices)
        {
            var inquire = new QuestionExtendedList<Dictionary<ConsoleKey, TResult>, TResult>(message);
            inquire.Choices = choices;

            inquire.WithInputValidation(value => { return inquire.Choices.ContainsKey(value); }, "Invalid key");

            inquire.Parse(answer =>
            {
                return inquire.Choices[answer];
            });

            return inquire;
        }

        public static QuestionInput<T> Input<T>(string message) where T : struct
        {
            var inquire = new QuestionInput<T>(message);

            inquire.WithInputValidation(value => { return string.IsNullOrEmpty(value) == false || inquire.HasDefaultValue; }, "Empty line");
            inquire.WithInputValidation(value => { return value.ToN<T>().HasValue; }, value => { return $"Cannot parse {value} to {typeof(T)}"; });

            inquire.Parse(answer =>
            {
                return answer.To<T>();
            });

            return inquire;
        }

        public static QuestionInput<string> Input(string message)
        {
            var inquire = new QuestionInput<string>(message);

            inquire.WithValidation(value => { return string.IsNullOrEmpty(value) == false || inquire.HasDefaultValue; }, "Empty line");

            inquire.Parse(answer =>
            {
                return answer;
            });

            return inquire;
        }

        public static QuestionList<TResult> List<TResult>(string message, List<TResult> choices)
        {
            var inquire = new QuestionList<TResult>(message);
            inquire.Choices = choices;

            inquire.Parse(answer =>
            {
                return inquire.Choices[answer - 1];
            });

            return inquire;
        }

        public static QuestionPassword<string> Password(string message)
        {
            var inquire = new QuestionPassword<string>(message);
            inquire.WithValidation(value => { return string.IsNullOrEmpty(value) == false || inquire.HasDefaultValue; }, "Empty line");

            inquire.Parse(answer =>
            {
                return answer;
            });

            return inquire;
        }

        public static QuestionRawList<TResult> RawList<TResult>(string message, List<TResult> choices)
        {
            var inquire = new QuestionRawList<TResult>(message);
            inquire.Choices = choices;

            inquire.WithInputValidation(value => { return value > 0 && value <= inquire.Choices.Count; }, value => { return $"Choosen number must be between 1 and {inquire.Choices.Count}"; });

            inquire.Parse(answer =>
            {
                return inquire.Choices[answer - 1];
            });

            return inquire;
        }
    }
}
