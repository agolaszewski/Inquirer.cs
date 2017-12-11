using System;
using InquirerCS.Questions;
using InquirerCS.Traits;

namespace InquirerCS.Builders
{
    public class InputStringBuilder : InputBuilder<Input<string>, string, string>
    {
        public InputStringBuilder(string message)
        {
            this.RenderQuestion(message, this, this);
            this.Parse(value => { return value; });
            this.Input(ConsoleKey.Escape);

            InputValidators.Add(value => { return string.IsNullOrEmpty(value) == false || Default.HasDefault; }, "Empty line");
        }

        public override Input<string> Build()
        {
            return new Input<string>(Confirm, RenderQuestion, Input, Parse, ResultValidators, InputValidators, DisplayError, Default, OnKey);
        }
    }
}
