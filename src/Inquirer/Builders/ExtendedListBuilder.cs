using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using InquirerCS.Interfaces;
using InquirerCS.Questions;
using InquirerCS.Traits;

namespace InquirerCS.Builders
{
    public class ExtendedListBuilder<TResult> : InputBuilder<ExtendedList<TResult>, ConsoleKey, TResult>, IRenderChoicesTrait<TResult>
    {
        internal ExtendedListBuilder(string message, Dictionary<ConsoleKey, TResult> choices, IConsole console) : base(console)
        {
            Console = console;
            Choices = choices;

            this.RenderQuestion(message, this, this, console);
            this.Parse(Choices);
            this.RenderChoices(Choices, this, Console);
            this.Input(Console, true, choices.Keys.ToArray());
        }

        [EditorBrowsable(EditorBrowsableState.Never)]
        public Dictionary<ConsoleKey, TResult> Choices { get; set; }

        [EditorBrowsable(EditorBrowsableState.Never)]
        public IRenderChoices<TResult> RenderChoices { get; set; }

        public override ExtendedList<TResult> Build()
        {
            return new ExtendedList<TResult>(Choices, Default, Confirm, RenderQuestion, Input, Parse, RenderChoices, ResultValidators, InputValidators, DisplayError, OnKey);
        }

        public ExtendedListBuilder<TResult> WithConfirmation()
        {
            this.Confirm(this, Console);
            return this;
        }

        public ExtendedListBuilder<TResult> WithConvertToString(Func<TResult, string> fn)
        {
            this.ConvertToString(fn);
            return this;
        }

        public ExtendedListBuilder<TResult> WithValidation(Func<TResult, bool> fn, Func<TResult, string> errorMessageFn)
        {
            ResultValidators.Add(fn, errorMessageFn);
            return this;
        }

        public ExtendedListBuilder<TResult> WithValidation(Func<TResult, bool> fn, string errorMessage)
        {
            ResultValidators.Add(fn, errorMessage);
            return this;
        }
    }
}
