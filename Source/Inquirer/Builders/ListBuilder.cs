using System;
using System.Collections.Generic;
using System.Linq;
using InquirerCS.Components;
using InquirerCS.Interfaces;
using InquirerCS.Questions;

namespace InquirerCS.Builders
{
    public class ListBuilder<TResult> : IBuilder<Listing<TResult>, TResult> where TResult : IComparable
    {
        private List<TResult> _choices;

        private DisplayChoices<TResult> _displayChoices;

        private IRenderQuestionComponent _displayQuestionComponent;

        private IDisplayErrorComponent _errorDisplay;

        private Extensions<TResult> _extensions = new Extensions<TResult>();

        private IWaitForInputComponent<StringOrKey> _inputComponent;

        private string _message;

        private IOnKey _onKey;

        private IParseComponent<int, TResult> _parseComponent;

        public ListBuilder(string message, IEnumerable<TResult> choices)
        {
            _message = message;
            _choices = choices.ToList();
        }

        public Listing<TResult> Build()
        {
            _extensions.Build();

            _displayQuestionComponent = new DisplayQuestion<TResult>(_message, _extensions.Convert, _extensions.Default);
            _inputComponent = new ReadConsoleKey();
            _parseComponent = new ParseListComponent<TResult>(_choices);
            _displayChoices = new DisplayChoices<TResult>(_choices, _extensions.Convert);
            _errorDisplay = new DisplayErrorCompnent();

            return new Listing<TResult>(_choices, _extensions.Confirm, _displayQuestionComponent, _inputComponent, _parseComponent, _displayChoices, _extensions.Validators, _errorDisplay, _onKey);
        }

        public ListBuilder<TResult> ConvertToString(Func<TResult, string> fn)
        {
            _extensions.ConvertToStringComponentFn = () =>
            {
                return new ConvertToStringComponent<TResult>(fn);
            };

            return this;
        }

        public PagedListBuilder<TResult> Page(int pageSize)
        {
            return new PagedListBuilder<TResult>(_message, _choices, pageSize, _extensions);
        }

        public TResult Prompt()
        {
            return Build().Prompt();
        }

        public ListBuilder<TResult> WithConfirmation()
        {
            _extensions.ConfirmComponentFn = () =>
            {
                return new ConfirmComponent<TResult>(_extensions.Convert);
            };

            return this;
        }

        public ListBuilder<TResult> WithDefaultValue(TResult defaultValue)
        {
            _extensions.DefaultValueComponentFn = () =>
            {
                return new DefaultListValueComponent<TResult>(_choices, defaultValue);
            };

            return this;
        }

        public ListBuilder<TResult> WithValidation(Func<TResult, bool> fn, Func<TResult, string> errorMessageFn)
        {
            _extensions.Validators.Add(fn, errorMessageFn);
            return this;
        }

        public ListBuilder<TResult> WithValidation(Func<TResult, bool> fn, string errorMessage)
        {
            _extensions.Validators.Add(fn, errorMessage);
            return this;
        }
    }
}
