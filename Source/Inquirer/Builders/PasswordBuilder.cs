using InquirerCS.Components;
using InquirerCS.Questions;
using InquirerCS.Traits;

namespace InquirerCS.Builders
{
    public class PasswordBuilder : InputBuilder<Input<string>, string, string>
    {
        internal PasswordBuilder(string message, IConsole console) : base(console)
        {
            this.Confirm();
            this.RenderQuestion(message, this, this, console);
            this.Parse(value => { return value; });
            this.PasswordInput(Console);

            InputValidators.Add(value => { return string.IsNullOrEmpty(value) == false || Default.HasDefault; }, "Empty line");
        }

        public override Input<string> Build()
        {
            return new Input<string>(Confirm, RenderQuestion, Input, Parse, ResultValidators, InputValidators, DisplayError, Default, OnKey);
        }

        public InputBuilder<Input<string>, string, string> WithConfirmation()
        {
            Confirm = new ConfirmPasswordComponent(Console, this);
            return this;
        }
    }
}
