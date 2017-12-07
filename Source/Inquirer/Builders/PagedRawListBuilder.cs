using System;
using System.Collections.Generic;
using System.Linq;
using InquirerCS.Components;
using InquirerCS.Interfaces;
using InquirerCS.Questions;

namespace InquirerCS.Builders
{
    public class PagedRawListBuilder<TResult> : IBuilder<TResult> where TResult : IComparable
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

        private IWaitForInputComponent<StringOrKey> _inputComponent;

        private string _message;

        private int _pageSize;

        private IPagingComponent<TResult> _pagingComponent;

        private IParseComponent<string, TResult> _parseComponent;

        private IValidateComponent<string> _validationInputComponent;

        private IValidateComponent<TResult> _validationResultComponent;

        public PagedRawListBuilder(
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
            _validationInputComponent = new ValidationComponent<string>();
        }

        public PagedRawListBuilder<TResult> ConvertToString(Func<TResult, string> fn)
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
            _inputComponent = new StringOrKeyInputComponent(ConsoleKey.LeftArrow, ConsoleKey.RightArrow);

            _parseComponent = new ParseComponent<string, TResult>(value =>
            {
                return _pagingComponent.CurrentPage[value.To<int>() - 1];
            });

            _displayChoices = new DisplaPagedRawChoices<TResult>(_pagingComponent, _convertToStringComponent);

            _validationInputComponent.Add(value => { return string.IsNullOrEmpty(value) == false || _defaultValueComponent.HasDefaultValue; }, "Empty line");
            _validationInputComponent.Add(value => { return value.ToN<int>().HasValue; }, value => { return $"Cannot parse {value} to {typeof(TResult)}"; });
            _validationInputComponent.Add(
            value =>
            {
                var index = value.To<int>();
                return index > 0 && index <= _choices.Count;
            },
            value =>
            {
                return $"Choosen number must be between 1 and {_pagingComponent.CurrentPage.Count}";
            });

            _errorDisplay = new DisplayErrorCompnent();

            return new PagedRawList<TResult>(_pagingComponent, _confirmComponent, _displayQuestionComponent, _inputComponent, _parseComponent, _displayChoices, _validationResultComponent, _validationInputComponent, _errorDisplay).Prompt();
        }

        public PagedRawListBuilder<TResult> WithConfirmation()
        {
            _confirmComponentFn = () =>
            {
                return new ConfirmComponent<TResult>(_convertToStringComponentFn());
            };

            return this;
        }

        public PagedRawListBuilder<TResult> WithDefaultValue(TResult defaultValue)
        {
            _defaultValueComponentFn = () =>
            {
                return new DefaultListValueComponent<TResult>(_choices, defaultValue);
            };

            return this;
        }

        public PagedRawListBuilder<TResult> WithValidation(Func<TResult, bool> fn, Func<TResult, string> errorMessageFn)
        {
            _validationResultComponent.Add(fn, errorMessageFn);
            return this;
        }

        public PagedRawListBuilder<TResult> WithValidation(Func<TResult, bool> fn, string errorMessage)
        {
            _validationResultComponent.Add(fn, errorMessage);
            return this;
        }
    }
}
