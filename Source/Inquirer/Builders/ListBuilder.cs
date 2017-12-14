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
        private List<TResult> _choices;

        public ListBuilder(string message, IEnumerable<TResult> choices, IConsole console) : base(console)
        {
            _choices = choices.ToList();
            _console = console;

            this.RenderQuestion(message, this, this, console);
            this.Parse(_choices);
            this.RenderChoices(_choices, this, _console);
            this.Parse(_choices);
            this.Input(_console, true, ConsoleKey.Enter, ConsoleKey.DownArrow, ConsoleKey.UpArrow);
        }

        public IRenderChoices<TResult> RenderChoices { get; set; }

        public override ConsoleList<TResult> Build()
        {
            return new ConsoleList<TResult>(_choices, Confirm, RenderQuestion, Input, Parse, RenderChoices, ResultValidators, DisplayError, OnKey);
        }

        public override InputBuilder<ConsoleList<TResult>, int, TResult> WithDefaultValue(TResult defaultValue)
        {
            Default = new DefaultListValueComponent<TResult>(_choices, defaultValue);
            return this;
        }
    }
}