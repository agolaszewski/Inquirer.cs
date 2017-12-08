using System;
using System.Linq;
using InquirerCS.Components;
using InquirerCS.Interfaces;
using InquirerCS.Questions;

namespace InquirerCS.Builders
{
    public class ExtendedBuilder : Builder<InputKey<ConsoleKey>, ConsoleKey, ConsoleKey>
    {
        private string _message;

        private IOnKey _onKey;

        private ConsoleKey[] _params;

        public ExtendedBuilder(string message, params ConsoleKey[] @params)
        {
            _message = message;
            _params = @params;
        }

        public override InputKey<ConsoleKey> Build()
        {
            _convertToStringComponent = new ConvertToStringComponent<ConsoleKey>();

            _defaultValueComponent = _defaultValueComponentFn() ?? new DefaultValueComponent<ConsoleKey>();
            _confirmComponent = _confirmComponentFn() ?? new NoConfirmationComponent<ConsoleKey>();

            _displayQuestionComponent = new DisplayQuestion<ConsoleKey>(_message, _convertToStringComponent, _defaultValueComponent);
            _inputComponent = new StringOrKeyInputComponent();
            _parseComponent = new ParseComponent<ConsoleKey, ConsoleKey>(value =>
            {
                return value;
            });

            _validationInputComponent = new ValidationComponent<ConsoleKey>();
            _validationInputComponent.Add(
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

            return new InputKey<ConsoleKey>(_confirmComponent, _displayQuestionComponent, _inputComponent, _parseComponent, _validationResultComponent, _validationInputComponent, _errorDisplay, _defaultValueComponent, _onKey);
        }

        public override ConsoleKey Prompt()
        {
            return Build().Prompt();
        }

        public ExtendedBuilder WithConfirmation()
        {
            _confirmComponentFn = () =>
            {
                return new ConfirmComponent<ConsoleKey>(_convertToStringComponentFn());
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

        public ExtendedBuilder WithValidation(Func<ConsoleKey, bool> fn, Func<ConsoleKey, string> errorMessageFn)
        {
            _validationResultComponent.Add(fn, errorMessageFn);
            return this;
        }

        public ExtendedBuilder WithValidation(Func<ConsoleKey, bool> fn, string errorMessage)
        {
            _validationResultComponent.Add(fn, errorMessage);
            return this;
        }
    }
}
