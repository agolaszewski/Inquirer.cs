using InquirerCS.Components;
using InquirerCS.Questions;
using InquirerCS.Traits;

namespace InquirerCS.Builders
{
    public class PasswordBuilder : InputBuilder<Input<string>, string, string>
    {
        public PasswordBuilder(string message, IConsole console)
        {
            _console = console;

            this.Confirm();
            this.RenderQuestion(message, this, this);
            this.Parse(value => { return value; });
            this.PasswordInput();

            InputValidators.Add(value => { return string.IsNullOrEmpty(value) == false || Default.HasDefault; }, "Empty line");
        }

        public override Input<string> Build()
        {
            return new Input<string>(Confirm, RenderQuestion, Input, Parse, ResultValidators, InputValidators, DisplayError, Default, OnKey);
        }

        public override InputBuilder<Input<string>, string, string> WithConfirmation()
        {
            Confirm = new ConfirmPasswordComponent(_console);
            return this;
        }
    }
}