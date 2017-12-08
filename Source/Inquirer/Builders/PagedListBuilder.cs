using System;
using System.Collections.Generic;
using InquirerCS.Components;
using InquirerCS.Interfaces;
using InquirerCS.Questions;

namespace InquirerCS.Builders
{
    public class PagedListBuilder<TResult> : IBuilder<PagedList<TResult>, TResult> where TResult : IComparable
    {
        private List<TResult> _choices;

        private IRenderChoices<TResult> _displayChoices;

        private IDisplayQuestionComponent _displayQuestionComponent;

        private IDisplayErrorComponent _errorDisplay;

        private Extensions<TResult> _extensions;

        private IWaitForInputComponent<StringOrKey> _inputComponent;

        private string _message;

        private IOnKey _onKey;

        private int _pageSize;

        private IPagingComponent<TResult> _pagingComponent;

        private IParseComponent<int, TResult> _parseComponent;

        public PagedListBuilder(
            string message,
            List<TResult> choices,
            int pageSize,
            Extensions<TResult> extensions)
        {
            _choices = choices;
            _pageSize = pageSize;

            _message = message;

            _extensions = extensions;
        }

        public PagedList<TResult> Build()
        {
            _extensions.Build();

            _pagingComponent = new PagingComponent<TResult>(_choices, _pageSize);

            _displayQuestionComponent = new DisplayQuestion<TResult>(_message, _extensions.Convert, _extensions.Default);
            _inputComponent = new ReadConsoleKey();
            _parseComponent = new ParseListComponent<TResult>(_choices);
            _displayChoices = new DisplayPagedListChoices<TResult>(_pagingComponent, _extensions.Convert);
            _errorDisplay = new DisplayErrorCompnent();

            return new PagedList<TResult>(_pagingComponent, _extensions.Confirm, _displayQuestionComponent, _inputComponent, _parseComponent, _displayChoices, _extensions.Validators, _errorDisplay, _onKey);
        }

        public PagedListBuilder<TResult> ConvertToString(Func<TResult, string> fn)
        {
            _extensions.ConvertToStringComponentFn = () =>
            {
                return new ConvertToStringComponent<TResult>(fn);
            };

            return this;
        }

        public TResult Prompt()
        {
            return Build().Prompt();
        }

        public PagedListBuilder<TResult> WithConfirmation()
        {
            _extensions.ConfirmComponentFn = () =>
            {
                return new ConfirmComponent<TResult>(_extensions.Convert);
            };

            return this;
        }

        public PagedListBuilder<TResult> WithDefaultValue(TResult defaultValue)
        {
            _extensions.DefaultValueComponentFn = () =>
            {
                return new DefaultListValueComponent<TResult>(_choices, defaultValue);
            };

            return this;
        }

        public PagedListBuilder<TResult> WithValidation(Func<TResult, bool> fn, Func<TResult, string> errorMessageFn)
        {
            _extensions.Validators.Add(fn, errorMessageFn);
            return this;
        }

        public PagedListBuilder<TResult> WithValidation(Func<TResult, bool> fn, string errorMessage)
        {
            _extensions.Validators.Add(fn, errorMessage);
            return this;
        }
    }
}
