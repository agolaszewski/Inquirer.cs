using System;
using InquirerCS.Components;
using InquirerCS.Interfaces;
using InquirerCS.Questions;

namespace InquirerCS.Builders
{
    public class InputKeyBuilder<TResult>
    {
        private IConfirmComponent<TResult> _confirmComponent;
        private IConvertToStringComponent<TResult> _convertToString;
        private IDefaultValueComponent<TResult> _defaultComponent;
        private IDisplayQuestionComponent _displayQuestionComponent;
        private IDisplayErrorComponent _errorDisplay;
        private IWaitForInputComponent<ConsoleKey> _inputComponent;
        private IMessageComponent _msgComponent;
        private IParseComponent<ConsoleKey, TResult> _parseComponent;
        private IValidateComponent<ConsoleKey> _validationInputComponent;
        private IValidateComponent<TResult> _validationResultComponent;

        private Func<IConfirmComponent<TResult>> _confirmComponentFn = () => { return null; };
        private Func<IConvertToStringComponent<TResult>> _convertToStringComponentFn = () => { return null; };
        private Func<IDefaultValueComponent<TResult>> _defaultValueComponentFn = () => { return null; };

        public TResult Prompt(string message)
        {
            _convertToString = _convertToStringComponentFn() ?? new ConvertToStringComponent<TResult>();
            _defaultComponent = _defaultValueComponentFn() ?? new DefaultValueComponent<TResult>();
            _confirmComponent = _confirmComponentFn() ?? new NoConfirmationComponent<TResult>();

            _msgComponent = new MessageComponent(message);

            _displayQuestionComponent = new DisplayQuestion<TResult>(_msgComponent, _convertToString, _defaultComponent);
            _inputComponent = new ReadConsoleKey();

            _validationInputComponent = new ValidationComponent<ConsoleKey>();
            _validationResultComponent = new ValidationComponent<TResult>();
            _errorDisplay = new DisplayErrorCompnent();

            return new InputKey<TResult>(_confirmComponent, _displayQuestionComponent, _inputComponent, _parseComponent, _validationResultComponent, _validationInputComponent, _errorDisplay, _defaultComponent).Prompt();
        }

        public InputKeyBuilder<TResult> WithDefaultValue(TResult defaultValues)
        {
            _defaultValueComponentFn = () =>
            {
                return new DefaultValueComponent<TResult>(defaultValues);
            };

            return this;
        }

        public InputKeyBuilder<TResult> ConvertToString(Func<TResult, string> convertFn)
        {
            _convertToStringComponentFn = () =>
            {
                return new ConvertToStringComponent<TResult>(convertFn);
            };

            return this;
        }

        public InputKeyBuilder<TResult> WithConfirmation()
        {
            _confirmComponentFn = () =>
            {
                return new ConfirmComponent<TResult>(_convertToString);
            };

            return this;
        }
    }
}