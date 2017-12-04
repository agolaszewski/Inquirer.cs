using System;
using System.Collections.Generic;
using System.Linq;
using InquirerCS.Components;
using InquirerCS.Interfaces;
using InquirerCS.Questions;

namespace InquirerCS.Builders
{
    public class ListBuilder<TResult> : IBuilder<TResult> where TResult : IComparable
    {
        private List<TResult> _choices;

        private IConfirmComponent<TResult> _confirmComponent;

        private Func<IConfirmComponent<TResult>> _confirmComponentFn = () => { return null; };

        private IConvertToStringComponent<TResult> _convertToStringComponent;

        private Func<IConvertToStringComponent<TResult>> _convertToStringComponentFn = () => { return null; };

        private IDefaultValueComponent<TResult> _defaultValueComponent;

        private Func<IDefaultValueComponent<TResult>> _defaultValueComponentFn = () => { return null; };

        private DisplayChoices<TResult> _displayChoices;

        private IDisplayQuestionComponent _displayQuestionComponent;

        private IDisplayErrorComponent _errorDisplay;

        private IWaitForInputComponent<ConsoleKey> _inputComponent;

        private string _message;

        private IParseComponent<int, TResult> _parseComponent;

        private IValidateComponent<TResult> _validationResultComponent;

        public ListBuilder(string message, IEnumerable<TResult> choices)
        {
            _message = message;
            _choices = choices.ToList();
            _validationResultComponent = new ValidationComponent<TResult>();
        }

        public TResult Prompt()
        {
            _convertToStringComponent = _convertToStringComponentFn() ?? new ConvertToStringComponent<TResult>();
            _defaultValueComponent = _defaultValueComponentFn() ?? new DefaultValueComponent<TResult>();
            _confirmComponent = _confirmComponentFn() ?? new NoConfirmationComponent<TResult>();

            _displayQuestionComponent = new DisplayQuestion<TResult>(_message, _convertToStringComponent, _defaultValueComponent);
            _inputComponent = new ReadConsoleKey();
            _parseComponent = new ParseListComponent<TResult>(_choices);
            _displayChoices = new DisplayChoices<TResult>(_choices, _convertToStringComponent);
            _errorDisplay = new DisplayErrorCompnent();

            return new Listing<TResult>(_choices, _confirmComponent, _displayQuestionComponent, _inputComponent, _parseComponent, _displayChoices, _validationResultComponent, _errorDisplay).Prompt();
        }

        public ListBuilder<TResult> ConvertToString(Func<TResult, string> fn)
        {
            _convertToStringComponentFn = () =>
            {
                return new ConvertToStringComponent<TResult>(fn);
            };

            return this;
        }

        public ListBuilder<TResult> WithConfirmation()
        {
            _confirmComponentFn = () =>
            {
                return new ConfirmComponent<TResult>(_convertToStringComponent);
            };

            return this;
        }

        public ListBuilder<TResult> WithDefaultValue(TResult defaultValue)
        {
            _defaultValueComponentFn = () =>
            {
                return new DefaultListValueComponent<TResult>(_choices, defaultValue);
            };

            return this;
        }

        public ListBuilder<TResult> WithValidation(Func<TResult, bool> fn, Func<TResult, string> errorMessageFn)
        {
            _validationResultComponent.Add(fn, errorMessageFn);
            return this;
        }

        public ListBuilder<TResult> WithValidation(Func<TResult, bool> fn, string errorMessage)
        {
            _validationResultComponent.Add(fn, errorMessage);
            return this;
        }
    }
}