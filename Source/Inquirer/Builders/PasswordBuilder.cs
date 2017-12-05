using System;
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
            _validationInputComponent = new ValidationComponent<string>();
            _validationResultComponent = new ValidationComponent<string>();
        }

        public override string Prompt()
        {
            _convertToStringComponent = _convertToStringComponentFn() ?? new ConvertToStringComponent<string>();
            _defaultValueComponent = _defaultValueComponentFn() ?? new DefaultValueComponent<string>();
            _confirmComponent = _confirmComponentFn() ?? new NoConfirmationComponent<string>();

            _displayQuestionComponent = new DisplayQuestion<string>(_message, _convertToStringComponent, _defaultValueComponent);
            _inputComponent = new HideReadStringComponent();
            _parseComponent = new ParseComponent<string, string>(value =>
            {
                return value;
            });
            _confirmComponent = new ConfirmPasswordComponent(_inputComponent);
            _validationInputComponent.Add(value => { return string.IsNullOrEmpty(value) == false || _defaultValueComponent.HasDefaultValue; }, "Empty line");

            _errorDisplay = new DisplayErrorCompnent();

            return new Input<string>(_confirmComponent, _displayQuestionComponent, _inputComponent, _parseComponent, _validationResultComponent, _validationInputComponent, _errorDisplay, _defaultValueComponent).Prompt();
        }

        public PasswordBuilder WithConfirmation()
        {
            _confirmComponentFn = () =>
            {
                return new ConfirmComponent<string>(_convertToStringComponent);
            };

            return this;
        }

        public PasswordBuilder WithDefaultValue(string defaultValues)
        {
            _defaultValueComponentFn = () =>
            {
                return new DefaultValueComponent<string>(defaultValues);
            };

            return this;
        }

        public PasswordBuilder WithValidation(Func<string, bool> fn, Func<string, string> errorMessageFn)
        {
            _validationResultComponent.Add(fn, errorMessageFn);
            return this;
        }

        public PasswordBuilder WithValidation(Func<string, bool> fn, string errorMessage)
        {
            _validationResultComponent.Add(fn, errorMessage);
            return this;
        }
    }
}