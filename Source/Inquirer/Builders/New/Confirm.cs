using System;
using InquirerCS.Questions;
using InquirerCS.Traits;

namespace InquirerCS.Builders.New
{
    public class ConfirmBuilder : InputBuilder<InputKey<bool>, ConsoleKey, bool>
    {
        public ConfirmBuilder(string message)
        {
            this.RenderConfirmQuestion(message, this, this);
            this.Parse(value => value == ConsoleKey.Y);
            this.ConvertToString(value =>
            {
                return value ? "yes" : "no";
            });
            this.Input(ConsoleKey.Y, ConsoleKey.N, ConsoleKey.Enter);
        }

        public override InputKey<bool> Build()
        {
            return new InputKey<bool>(Confirm, RenderQuestion, Input, Parse, ResultValidators, InputValidators, DisplayError, Default, OnKey);
        }
    }
}
