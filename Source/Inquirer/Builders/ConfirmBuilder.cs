using System;
using InquirerCS.Components;
using InquirerCS.Questions;

namespace InquirerCS.Builders
{
    public class ConfirmBuilder : Builder<ConsoleKey, bool>
    {
        private string _message;

        public ConfirmBuilder(string message)
        {
            _message = message + " [y/n]";
        }

        public ConfirmBuilder AddValidator(Func<bool, bool> fn, Func<bool, string> errorMessageFn)
        {
            _validationResultComponent.AddValidator(fn, errorMessageFn);
            return this;
        }

        public ConfirmBuilder AddValidator(Func<bool, bool> fn, string errorMessage)
        {
            _validationResultComponent.AddValidator(fn, errorMessage);
            return this;
        }

        public override bool Build()
        {
            _convertToString = new ConvertToStringComponent<bool>(value =>
            {
                return value ? "yes" : "no";
            });

            _defaultComponent = _defaultValueComponentFn() ?? new DefaultValueComponent<bool>();
            _confirmComponent = _confirmComponentFn() ?? new NoConfirmationComponent<bool>();

            _displayQuestionComponent = new DisplayQuestion<bool>(_msgComponent, _convertToString, _defaultComponent);
            _inputComponent = new ReadConsoleKey();
            _parseComponent = new ParseComponent<ConsoleKey, bool>(value =>
            {
                return value == ConsoleKey.Y;
            });

            _validationInputComponent = new ValidationComponent<ConsoleKey>();
            _validationResultComponent = new ValidationComponent<bool>();
            _errorDisplay = new DisplayErrorCompnent();

            return new InputKey<bool>(_confirmComponent, _displayQuestionComponent, _inputComponent, _parseComponent, _validationResultComponent, _validationInputComponent, _errorDisplay, _defaultComponent).Prompt();
        }

        public ConfirmBuilder WithConfirmation()
        {
            _confirmComponentFn = () =>
            {
                return new ConfirmComponent<bool>(_convertToString);
            };

            return this;
        }

        public ConfirmBuilder WithDefaultValue(bool defaultValues)
        {
            _defaultValueComponentFn = () =>
            {
                return new DefaultValueComponent<bool>(defaultValues);
            };

            return this;
        }
    }
}
