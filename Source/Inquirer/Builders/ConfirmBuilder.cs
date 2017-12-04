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
            _validationInputComponent = new ValidationComponent<ConsoleKey>();
            _validationResultComponent = new ValidationComponent<bool>();
        }

        public override bool Prompt()
        {
            _convertToStringComponent = new ConvertToStringComponent<bool>(value =>
             {
                 return value ? "yes" : "no";
             });

            _defaultValueComponent = _defaultValueComponentFn() ?? new DefaultValueComponent<bool>();
            _confirmComponent = _confirmComponentFn() ?? new NoConfirmationComponent<bool>();

            _displayQuestionComponent = new DisplayQuestion<bool>(_message, _convertToStringComponent, _defaultValueComponent);
            _inputComponent = new ReadConsoleKey();
            _parseComponent = new ParseComponent<ConsoleKey, bool>(value =>
            {
                return value == ConsoleKey.Y;
            });

            _errorDisplay = new DisplayErrorCompnent();

            return new InputKey<bool>(_confirmComponent, _displayQuestionComponent, _inputComponent, _parseComponent, _validationResultComponent, _validationInputComponent, _errorDisplay, _defaultValueComponent).Prompt();
        }

        public ConfirmBuilder WithConfirmation()
        {
            _confirmComponentFn = () =>
            {
                return new ConfirmComponent<bool>(_convertToStringComponent);
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

        public ConfirmBuilder WithValidation(Func<bool, bool> fn, Func<bool, string> errorMessageFn)
        {
            _validationResultComponent.Add(fn, errorMessageFn);
            return this;
        }

        public ConfirmBuilder WithValidation(Func<bool, bool> fn, string errorMessage)
        {
            _validationResultComponent.Add(fn, errorMessage);
            return this;
        }
    }
}