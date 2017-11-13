using System;
using System.Collections.Generic;

namespace InquirerCS
{
    public abstract class QuestionSingleChoiceBase<TInput, TResult> : QuestionBase<TResult>
    {
        public QuestionSingleChoiceBase(string question) : base(question)
        {
        }

        internal List<Tuple<Func<TInput, bool>, Func<TInput, string>>> ValidatorsTInput { get; set; } = new List<Tuple<Func<TInput, bool>, Func<TInput, string>>>();

        internal List<Tuple<Func<TResult, bool>, Func<TResult, string>>> ValidatorsTResults { get; set; } = new List<Tuple<Func<TResult, bool>, Func<TResult, string>>>();

        public QuestionSingleChoiceBase<TInput, TResult> WithValidation(Func<TInput, bool> fn, Func<TInput, string> errorMessageFn)
        {
            ValidatorsTInput.Add(new Tuple<Func<TInput, bool>, Func<TInput, string>>(fn, errorMessageFn));
            return this;
        }

        public QuestionSingleChoiceBase<TInput, TResult> WithValidation(Func<TInput, bool> fn, string errorMessage)
        {
            ValidatorsTInput.Add(new Tuple<Func<TInput, bool>, Func<TInput, string>>(fn, answers => { return errorMessage; }));
            return this;
        }

        public QuestionSingleChoiceBase<TInput, TResult> WithValidation(Func<TResult, bool> fn, Func<TResult, string> errorMessageFn)
        {
            ValidatorsTResults.Add(new Tuple<Func<TResult, bool>, Func<TResult, string>>(fn, errorMessageFn));
            return this;
        }

        public QuestionSingleChoiceBase<TInput, TResult> WithValidation(Func<TResult, bool> fn, string errorMessage)
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