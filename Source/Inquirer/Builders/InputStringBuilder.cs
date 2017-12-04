using System;
using InquirerCS.Components;
using InquirerCS.Questions;

namespace InquirerCS.Builders
{
    public class InputStringBuilder : Builder<string, string>
    {
        private string _message;

        public InputStringBuilder(string message)
        {
            _message = message;
        }

        public InputStringBuilder AddValidator(Func<string, bool> fn, Func<string, string> errorMessageFn)
        {
            _validationResultComponent.AddValidator(fn, errorMessageFn);
            return this;
        }

        public InputStringBuilder AddValidator(Func<string, bool> fn, string errorMessage)
        {
            _validationResultComponent.AddValidator(fn, errorMessage);
            return this;
        }

        public override string Prompt()
        {
            _convertToString = new ConvertToStringComponent<string>();

            _confirmComponent = new NoConfirmationComponent<string>();
            _defaultComponent = new DefaultValueComponent<string>();

            _displayQuestionComponent = new DisplayQuestion<string>(_message, _convertToString, _defaultComponent);
            _inputComponent = new ReadStringComponent();
            _parseComponent = new ParseComponent<string, string>(value =>
            {
                return value;
            });

            var validationInputComponent = new ValidationComponent<string>();
            validationInputComponent.AddValidator(value => { return string.IsNullOrEmpty(value) == false || _defaultComponent.HasDefaultValue; }, "Empty line");

            var validationResultComponent = new ValidationComponent<string>();
            var errorDisplay = new DisplayErrorCompnent();

            return new Input<string>(_confirmComponent, _displayQuestionComponent, _inputComponent, _parseComponent, validationResultComponent, validationInputComponent, errorDisplay, _defaultComponent).Prompt();
        }

        public InputStringBuilder WithConfirmation()
        {
            _confirmComponentFn = () =>
            {
                return new ConfirmComponent<string>(_convertToString);
            };

            return this;
        }

        public InputStringBuilder WithDefaultValue(string defaultValues)
        {
            _defaultValueComponentFn = () =>
            {
                return new DefaultValueComponent<string>(defaultValues);
            };

            return this;
        }
    }
}
