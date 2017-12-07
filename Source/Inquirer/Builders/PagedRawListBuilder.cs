using System;
using System.Collections.Generic;
using InquirerCS.Components;
using InquirerCS.Interfaces;
using InquirerCS.Questions;

namespace InquirerCS.Builders
{
    public class PagedRawListBuilder<TResult> : IBuilder<TResult> where TResult : IComparable
    {
        private List<TResult> _choices;

        private IRenderChoices<TResult> _displayChoices;

        private IDisplayQuestionComponent _displayQuestionComponent;

        private IDisplayErrorComponent _errorDisplay;
        private Extensions<TResult> _extensions;
        private IWaitForInputComponent<StringOrKey> _inputComponent;

        private string _message;

        private int _pageSize;

        private IPagingComponent<TResult> _pagingComponent;

        private IParseComponent<string, TResult> _parseComponent;

        private IValidateComponent<string> _validationInputComponent;

        public PagedRawListBuilder(
            string message,
            List<TResult> choices,
            int pageSize,
            Extensions<TResult> extensions)
        {
            _choices = choices;
            _pageSize = pageSize;

            _message = message;

            _extensions = extensions;

            _validationInputComponent = new ValidationComponent<string>();
        }

        public PagedRawListBuilder<TResult> ConvertToString(Func<TResult, string> fn)
        {
            _extensions.ConvertToStringComponentFn = () =>
            {
                return new ConvertToStringComponent<TResult>(fn);
            };

            return this;
        }

        public TResult Prompt()
        {
            _extensions.Build();

            _pagingComponent = new PagingComponent<TResult>(_choices, _pageSize);

            _displayQuestionComponent = new DisplayQuestion<TResult>(_message, _extensions.Convert, _extensions.Default);
            _inputComponent = new StringOrKeyInputComponent(ConsoleKey.LeftArrow, ConsoleKey.RightArrow);

            _parseComponent = new ParseComponent<string, TResult>(value =>
            {
                return _pagingComponent.CurrentPage[value.To<int>() - 1];
            });

            _displayChoices = new DisplaPagedRawChoices<TResult>(_pagingComponent, _extensions.Convert);

            _validationInputComponent.Add(value => { return string.IsNullOrEmpty(value) == false || _extensions.Default.HasDefaultValue; }, "Empty line");
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

            return new PagedRawList<TResult>(_pagingComponent, _extensions.Confirm, _displayQuestionComponent, _inputComponent, _parseComponent, _displayChoices, _extensions.Validators, _validationInputComponent, _errorDisplay).Prompt();
        }

        public PagedRawListBuilder<TResult> WithConfirmation()
        {
            _extensions.ConfirmComponentFn = () =>
            {
                return new ConfirmComponent<TResult>(_extensions.Convert);
            };

            return this;
        }

        public PagedRawListBuilder<TResult> WithDefaultValue(TResult defaultValue)
        {
            _extensions.DefaultValueComponentFn = () =>
            {
                return new DefaultListValueComponent<TResult>(_choices, defaultValue);
            };

            return this;
        }

        public PagedRawListBuilder<TResult> WithValidation(Func<TResult, bool> fn, Func<TResult, string> errorMessageFn)
        {
            _extensions.Validators.Add(fn, errorMessageFn);
            return this;
        }

        public PagedRawListBuilder<TResult> WithValidation(Func<TResult, bool> fn, string errorMessage)
        {
            _extensions.Validators.Add(fn, errorMessage);
            return this;
        }
    }
}