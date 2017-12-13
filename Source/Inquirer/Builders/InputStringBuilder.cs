using System;
using InquirerCS.Questions;
using InquirerCS.Traits;

namespace InquirerCS.Builders
{
    public class InputStringBuilder : InputBuilder<Input<string>, string, string>
    {
        public InputStringBuilder(string message, IConsole console) : base(console)
        {
            this.RenderQuestion(message, this, this, _console);
            this.Parse(value => { return value; });
            this.Input(_console, ConsoleKey.Escape);

            InputValidators.Add(value => { return string.IsNullOrEmpty(value) == false || Default.HasDefault; }, "Empty line");
        }

        public override Input<string> Build()
        {
            return new Input<string>(Confirm, RenderQuestion, Input, Parse, ResultValidators, InputValidators, DisplayError, Default, OnKey);
        }
    }
}
