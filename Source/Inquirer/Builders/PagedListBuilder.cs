using System;
using System.Collections.Generic;
using InquirerCS.Components;
using InquirerCS.Interfaces;
using InquirerCS.Questions;

namespace InquirerCS.Builders
{
    public class PagedListBuilder<TResult> : IBuilder<TResult> where TResult : IComparable
    {
        private List<TResult> _choices;

        private IConfirmComponent<TResult> _confirmComponent;

        private Func<IConfirmComponent<TResult>> _confirmComponentFn = () => { return null; };

        private IConvertToStringComponent<TResult> _convertToStringComponent;

        private Func<IConvertToStringComponent<TResult>> _convertToStringComponentFn = () => { return null; };

        private IDefaultValueComponent<TResult> _defaultValueComponent;

        private Func<IDefaultValueComponent<TResult>> _defaultValueComponentFn = () => { return null; };

        private IRenderChoices<TResult> _displayChoices;

        private IDisplayQuestionComponent _displayQuestionComponent;

        private IDisplayErrorComponent _errorDisplay;

        private IWaitForInputComponent<ConsoleKey> _inputComponent;

        private string _message;

        private int _pageSize;

        private IPagingComponent<TResult> _pagingComponent;

        private IParseComponent<int, TResult> _parseComponent;

        private IValidateComponent<TResult> _validationResultComponent;

        public PagedListBuilder(
            string message,
            List<TResult> choices,
            int pageSize,
            Func<IConvertToStringComponent<TResult>> convertToStringComponentFn,
            Func<IConfirmComponent<TResult>> confirmComponentFn,
            Func<IDefaultValueComponent<TResult>> defaultValueComponentFn)
        {
            _choices = choices;
            _pageSize = pageSize;

            _message = message;

            _convertToStringComponentFn = convertToStringComponentFn;
            _confirmComponentFn = confirmComponentFn;
            _defaultValueComponentFn = defaultValueComponentFn;

            _validationResultComponent = new ValidationComponent<TResult>();
        }

        public PagedListBuilder<TResult> ConvertToString(Func<TResult, string> fn)
        {
            _convertToStringComponentFn = () =>
            {
                return new ConvertToStringComponent<TResult>(fn);
            };

            return this;
        }

        public TResult Prompt()
        {
            _convertToStringComponent = _convertToStringComponentFn() ?? new ConvertToStringComponent<TResult>();
            _defaultValueComponent = _defaultValueComponentFn() ?? new DefaultValueComponent<TResult>();
            _confirmComponent = _confirmComponentFn() ?? new NoConfirmationComponent<TResult>();

            _pagingComponent = new PagingComponent<TResult>(_choices, _pageSize);

            _displayQuestionComponent = new DisplayQuestion<TResult>(_message, _convertToStringComponent, _defaultValueComponent);
            _inputComponent = new ReadConsoleKey();
            _parseComponent = new ParseListComponent<TResult>(_choices);
            _displayChoices = new DisplayPagedListChoices<TResult>(_pagingComponent, _convertToStringComponent);
            _errorDisplay = new DisplayErrorCompnent();

            return new PagedList<TResult>(_pagingComponent, _confirmComponent, _displayQuestionComponent, _inputComponent, _parseComponent, _displayChoices, _validationResultComponent, _errorDisplay).Prompt();
        }

        public PagedListBuilder<TResult> WithConfirmation()
        {
            _confirmComponentFn = () =>
            {
                return new ConfirmComponent<TResult>(_convertToStringComponentFn());
            };

            return this;
        }

        public PagedListBuilder<TResult> WithDefaultValue(TResult defaultValue)
        {
            _defaultValueComponentFn = () =>
            {
                return new DefaultListValueComponent<TResult>(_choices, defaultValue);
            };

            return this;
        }

        public PagedListBuilder<TResult> WithValidation(Func<TResult, bool> fn, Func<TResult, string> errorMessageFn)
        {
            _validationResultComponent.Add(fn, errorMessageFn);
            return this;
        }

        public PagedListBuilder<TResult> WithValidation(Func<TResult, bool> fn, string errorMessage)
        {
            _validationResultComponent.Add(fn, errorMessage);
            return this;
        }
    }
}
