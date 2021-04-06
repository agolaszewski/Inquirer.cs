using System;
using InquirerCS.Questions;
using InquirerCS.Traits;

namespace InquirerCS.Builders
{
    public class InputStructBuilder<TResult> : InputBuilder<Input<TResult>, string, TResult> where TResult : struct
    {
        internal InputStructBuilder(string message, IConsole console) : base(console)
        {
            this.RenderQuestion(message, this, this, console);
            this.Parse(value => { return value.To<TResult>(); });
            this.Input(Console, ConsoleKey.Escape);

            InputValidators.Add(value => { return string.IsNullOrEmpty(value) == false || Default.HasDefault; }, "Empty line");
            InputValidators.Add(value => { return value.ToN<TResult>().HasValue; }, value => { return $"Cannot parse {value} to {typeof(TResult)}"; });
        }

        public override Input<TResult> Build()
        {
            return new Input<TResult>(Confirm, RenderQuestion, Input, Parse, ResultValidators, InputValidators, DisplayError, Default, OnKey);
        }
    }
}
