using System;
using InquirerCS.Components;
using InquirerCS.Interfaces;
using InquirerCS.Questions;

namespace InquirerCS.Builders
{
    public class InputBuilder<TResult>
    {
        private IConfirmComponent<TResult> _confirmComponent;
        private IConvertToStringComponent<TResult> _convertToString;
        private IDefaultValueComponent<TResult> _defaultComponent;
        private IDisplayQuestionComponent _displayQuestionComponent;
        private IDisplayErrorComponent _errorDisplay;
        private IWaitForInputComponent<string> _inputComponent;
        private IMessageComponent _msgComponent;
        private IParseComponent<string, TResult> _parseComponent;
        private IValidateComponent<string> _validationInputComponent;
        private IValidateComponent<TResult> _validationResultComponent;

        public InputBuilder()
        {

        }

        public TResult Prompt(string message)
        {
            _convertToString = new ConvertToStringComponent<TResult>();
            _msgComponent = new MessageComponent(message);
            _confirmComponent = new NoConfirmationComponent<TResult>();
            _defaultComponent = new DefaultValueComponent<TResult>();

            _displayQuestionComponent = new DisplayQuestion<TResult>(_msgComponent, _convertToString, _defaultComponent);
            _inputComponent = new ReadStringComponent();
            _parseComponent = new ParseComponent<string, TResult>(value =>
            {
                return value.To<TResult>();
            });

            _validationInputComponent = new ValidationComponent<string>();
            _validationInputComponent.AddValidator(value => { return string.IsNullOrEmpty(value) == false || _defaultComponent.HasDefaultValue; }, "Empty line");

            _validationResultComponent = new ValidationComponent<TResult>();
            _errorDisplay = new DisplayErrorCompnent();

            return new Input<TResult>(_confirmComponent, _displayQuestionComponent, _inputComponent, _parseComponent, _validationResultComponent, _validationInputComponent, _errorDisplay, _defaultComponent).Prompt();
        }

        public InputBuilder<TResult> WithDefaultValue(TResult defaultValues)
        {
            _defaultComponent = new DefaultValueComponent<TResult>(defaultValues);
            return this;
        }

        public InputBuilder<TResult> ConvertToString(Func<TResult, string> convertFn)
        {
            _convertToString = new ConvertToStringComponent<TResult>(convertFn);
            return this;
        }

        public InputBuilder<TResult> WithConfirmation()
        {
            _confirmComponent = new ConfirmComponent<TResult>(_convertToString);
            return this;
        }
    }
}