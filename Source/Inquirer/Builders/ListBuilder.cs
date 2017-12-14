using System;
using System.Collections.Generic;
using System.Linq;
using InquirerCS.Components;
using InquirerCS.Interfaces;
using InquirerCS.Questions;
using InquirerCS.Traits;

namespace InquirerCS.Builders
{
    public class ListBuilder<TResult> : InputBuilder<ConsoleList<TResult>, int, TResult>, IRenderChoicesTrait<TResult> where TResult : IComparable
    {
        public ListBuilder(string message, IEnumerable<TResult> choices, IConsole console) : base(console)
        {
            Choices = choices.ToList();
            Console = console;

            this.RenderQuestion(message, this, this, console);
            this.Parse(Choices);
            this.RenderChoices(Choices, this, Console);
            this.Parse(Choices);
            this.Input(Console, true, ConsoleKey.Enter, ConsoleKey.DownArrow, ConsoleKey.UpArrow);
        }

        public List<TResult> Choices { get; set; }

        public IRenderChoices<TResult> RenderChoices { get; set; }

        public override ConsoleList<TResult> Build()
        {
            return new ConsoleList<TResult>(Choices, Confirm, RenderQuestion, Input, Parse, RenderChoices, ResultValidators, DisplayError, OnKey);
        }

        public PagedListBuilder<TResult> Page(int pageSize)
        {
            return new PagedListBuilder<TResult>(this, pageSize);
        }

        public new ListBuilder<TResult> WithConfirmation()
        {
            this.Confirm(this, Console);
            return this;
        }

        public new ListBuilder<TResult> WithConvertToString(Func<TResult, string> fn)
        {
            this.ConvertToString(fn);
            return this;
        }

        public new ListBuilder<TResult> WithValidation(Func<TResult, bool> fn, Func<TResult, string> errorMessageFn)
        {
            ResultValidators.Add(fn, errorMessageFn);
            return this;
        }

        public new ListBuilder<TResult> WithValidation(Func<TResult, bool> fn, string errorMessage)
        {
            ResultValidators.Add(fn, errorMessage);
            return this;
        }

        public new ListBuilder<TResult> WithDefaultValue(TResult defaultValue)
        {
            Default = new DefaultListValueComponent<TResult>(Choices, defaultValue);
            return this;
        }
    }
}