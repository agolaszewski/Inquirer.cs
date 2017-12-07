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
            _validationInputComponent = new ValidationComponent<string>();
            _validationResultComponent = new ValidationComponent<string>();
        }

        public override string Prompt()
        {
            _convertToStringComponent = _convertToStringComponentFn() ?? new ConvertToStringComponent<string>();
            _defaultValueComponent = _defaultValueComponentFn() ?? new DefaultValueComponent<string>();
            _confirmComponent = _confirmComponentFn() ?? new NoConfirmationComponent<string>();

            _displayQuestionComponent = new DisplayQuestion<string>(_message, _convertToStringComponent, _defaultValueComponent);
            _inputComponent = new ReadStringComponent();
            _parseComponent = new ParseComponent<string, string>(value =>
            {
                return value;
            });

            _validationInputComponent.Add(value => { return string.IsNullOrEmpty(value) == false || _defaultValueComponent.HasDefaultValue; }, "Empty line");
            _errorDisplay = new DisplayErrorCompnent();

            return new Input<string>(_confirmComponent, _displayQuestionComponent, _inputComponent, _parseComponent, _validationResultComponent, _validationInputComponent, _errorDisplay, _defaultValueComponent).Prompt();
        }

        public InputStringBuilder WithConfirmation()
        {
            _confirmComponentFn = () =>
            {
                return new ConfirmComponent<string>(_convertToStringComponentFn());
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

        public InputStringBuilder WithValidation(Func<string, bool> fn, Func<string, string> errorMessageFn)
        {
            _validationResultComponent.Add(fn, errorMessageFn);
            return this;
        }

        public InputStringBuilder WithValidation(Func<string, bool> fn, string errorMessage)
        {
            _validationResultComponent.Add(fn, errorMessage);
            return this;
        }
    }
}