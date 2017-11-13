using System;
using System.Collections.Generic;

namespace InquirerCS
{
    public abstract class QuestionSingleChoiceBase<TInput, TResult> : QuestionBase<TResult>
    {
        public QuestionSingleChoiceBase(string question) : base(question)
        {
        }

        protected List<Tuple<Func<TInput, bool>, Func<TInput, string>>> ValidatorsKey { get; set; } = new List<Tuple<Func<TInput, bool>, Func<TInput, string>>>();

        protected List<Tuple<Func<TResult, bool>, Func<TResult, string>>> ValidatorsTResults { get; set; } = new List<Tuple<Func<TResult, bool>, Func<TResult, string>>>();


        public QuestionSingleChoiceBase<TInput,TResult> WithValidatation(Func<TInput, bool> fn, Func<ConsoleKey, string> errorMessageFn)
        {
            ValidatorsKey.Add(new Tuple<Func<TInput, bool>, Func<TInput, string>>(fn, errorMessageFn));
            return this;
        }

        public QuestionInputKey<TResult> WithValidatation(Func<ConsoleKey, bool> fn, string errorMessage)
        {
            ValidatorsKey.Add(new Tuple<Func<ConsoleKey, bool>, Func<ConsoleKey, string>>(fn, answers => { return errorMessage; }));
            return this;
        }

        public QuestionInputKey<TResult> WithValidatation(Func<TResult, bool> fn, Func<TResult, string> errorMessageFn)
        {
            ValidatorsTResults.Add(new Tuple<Func<TResult, bool>, Func<TResult, string>>(fn, errorMessageFn));
            return this;
        }

        public QuestionInputKey<TResult> WithValidatation(Func<TResult, bool> fn, string errorMessage)
        {
            ValidatorsTResults.Add(new Tuple<Func<TResult, bool>, Func<TResult, string>>(fn, answers => { return errorMessage; }));
            return this;
        }

        internal Func<TResult, string> ConvertToStringFn { get; set; } = value => { return value.ToString(); };

        protected virtual void DisplayQuestion()
        {
            Console.Clear();
            ConsoleHelper.Write("[?] ", ConsoleColor.Yellow);
            var question = $"{Message} : ";
            if (HasDefaultValue)
            {
                question += $"[{ConvertToStringFn(DefaultValue)}] ";
            }

            ConsoleHelper.Write(question);
        }
    }
}