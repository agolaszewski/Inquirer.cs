using System;
using System.Linq;
using InquirerCS.Components;
using InquirerCS.Questions;

namespace InquirerCS.Builders
{
    public class ExtendedBuilder : Builder<ConsoleKey, ConsoleKey>
    {
        private string _message;

        private ConsoleKey[] _params;

        public ExtendedBuilder(string message, params ConsoleKey[] @params)
        {
            _message = message;
            _params = @params;
        }

        public ExtendedBuilder AddValidator(Func<ConsoleKey, bool> fn, Func<ConsoleKey, string> errorMessageFn)
        {
            _validationResultComponent.AddValidator(fn, errorMessageFn);
            return this;
        }

        public ExtendedBuilder AddValidator(Func<ConsoleKey, bool> fn, string errorMessage)
        {
            _validationResultComponent.AddValidator(fn, errorMessage);
            return this;
        }

        public override ConsoleKey Build()
        {
            _convertToString = new ConvertToStringComponent<ConsoleKey>();

            _defaultComponent = _defaultValueComponentFn() ?? new DefaultValueComponent<ConsoleKey>();
            _confirmComponent = _confirmComponentFn() ?? new NoConfirmationComponent<ConsoleKey>();

            _displayQuestionComponent = new DisplayQuestion<ConsoleKey>(_message, _convertToString, _defaultComponent);
            _inputComponent = new ReadConsoleKey();
            _parseComponent = new ParseComponent<ConsoleKey, ConsoleKey>(value =>
            {
                return value;
            });

            _validationInputComponent = new ValidationComponent<ConsoleKey>();
            _validationInputComponent.AddValidator(
            value =>
            {
                return _params.Any(p => p == value);
            },
            value =>
            {
                string keys = " Press : ";
                foreach (var key in _params)
                {
                    keys += $"[{(char)key}] ";
                }

                return keys;
            });

            _validationResultComponent = new ValidationComponent<ConsoleKey>();
            _errorDisplay = new DisplayErrorCompnent();

            return new InputKey<ConsoleKey>(_confirmComponent, _displayQuestionComponent, _inputComponent, _parseComponent, _validationResultComponent, _validationInputComponent, _errorDisplay, _defaultComponent).Prompt();
        }

        public ExtendedBuilder WithConfirmation()
        {
            _confirmComponentFn = () =>
            {
                return new ConfirmComponent<ConsoleKey>(_convertToString);
            };

            return this;
        }

        public ExtendedBuilder WithDefaultValue(ConsoleKey defaultValues)
        {
            _defaultValueComponentFn = () =>
            {
                return new DefaultValueComponent<ConsoleKey>(defaultValues);
            };

            return this;
        }
    }
}
