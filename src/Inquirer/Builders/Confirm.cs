using System;
using InquirerCS.Questions;
using InquirerCS.Traits;

namespace InquirerCS.Builders
{
    public class ConfirmBuilder : InputBuilder<InputKey<bool>, ConsoleKey, bool>
    {
        internal ConfirmBuilder(string message, IConsole console) : base(console)
        {
            this.RenderConfirmQuestion(message, this, this, console);
            this.Parse(value => value == ConsoleKey.Y);
            this.ConvertToString(value =>
            {
                return value ? "yes" : "no";
            });
            this.Input(Console, true, ConsoleKey.Y, ConsoleKey.N, ConsoleKey.Enter);
        }

        public override InputKey<bool> Build()
        {
            return new InputKey<bool>(Confirm, RenderQuestion, Input, Parse, ResultValidators, InputValidators, DisplayError, Default, OnKey);
        }
    }
}
