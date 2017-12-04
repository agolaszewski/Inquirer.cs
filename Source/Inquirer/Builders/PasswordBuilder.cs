using InquirerCS.Components;
using InquirerCS.Questions;

namespace InquirerCS.Builders
{
    public class PasswordBuilder : Builder<string, string>
    {
        private string _message;

        public PasswordBuilder(string message)
        {
            _message = message;
        }

        public override string Build()
        {
            _convertToString = new ConvertToStringComponent<string>();
            _defaultComponent = new DefaultValueComponent<string>();
            _displayQuestionComponent = new DisplayQuestion<string>(_msgComponent, _convertToString, _defaultComponent);
            _inputComponent = new HideReadStringComponent();
            _parseComponent = new ParseComponent<string, string>(value =>
             {
                 return value;
             });
            _confirmComponent = new ConfirmPasswordComponent(_inputComponent);
            _validationInputComponent = new ValidationComponent<string>();
            _validationInputComponent.AddValidator(value => { return string.IsNullOrEmpty(value) == false || _defaultComponent.HasDefaultValue; }, "Empty line");

            _validationResultComponent = new ValidationComponent<string>();
            _errorDisplay = new DisplayErrorCompnent();

            return new Input<string>(_confirmComponent, _displayQuestionComponent, _inputComponent, _parseComponent, _validationResultComponent, _validationInputComponent, _errorDisplay, _defaultComponent).Prompt();
        }
    }
}
