using System;
using System.Collections.Generic;
using System.Linq;
using InquirerCS.Components;
using InquirerCS.Interfaces;
using InquirerCS.Questions;

namespace InquirerCS.Builders
{
    public class PagedCheckboxBuilder<TResult> : IBuilder<PagedCheckbox<List<TResult>, TResult>, List<TResult>> where TResult : IComparable
    {
        private List<Selectable<TResult>> _choices;

        private IRenderQuestionComponent _displayQuestionComponent;

        private IDisplayErrorComponent _errorComponent;

        private ExtensionsCheckbox<TResult> _extensions;

        private IWaitForInputComponent<StringOrKey> _inputComponent;

        private string _message;

        private IOnKey _onKey;

        private int _pageSize;

        private IPagingComponent<Selectable<TResult>> _pagingComponent;

        private IParseComponent<Dictionary<int, List<Selectable<TResult>>>, List<TResult>> _parseComponent;

        private IRenderChoices<TResult> _renderchoices;

        public PagedCheckboxBuilder(string message, List<Selectable<TResult>> choices, int pageSize, ExtensionsCheckbox<TResult> extensions)
        {
            _choices = choices;
            _pageSize = pageSize;
            _message = message;
            _extensions = extensions;
        }

        public PagedCheckbox<List<TResult>, TResult> Build()
        {
            _extensions.Build();

            _pagingComponent = new PagingComponent<Selectable<TResult>>(_choices, _pageSize);

            _displayQuestionComponent = new DisplayListQuestion<List<TResult>, TResult>(_message, _extensions.Convert, _extensions.Default);

            _inputComponent = new ReadConsoleKey();
            _parseComponent = new ParseSelectablePagedListComponent<List<TResult>, TResult>(_pagingComponent.PagedChoices);
            _renderchoices = new DisplaySelectablePagedChoices<TResult>(_pagingComponent, _extensions.Convert);
            _errorComponent = new DisplayErrorCompnent();

            return new PagedCheckbox<List<TResult>, TResult>(_pagingComponent, _extensions.Confirm, _displayQuestionComponent, _inputComponent, _parseComponent, _renderchoices, _extensions.Validators, _errorComponent, _onKey);
        }

        public PagedCheckboxBuilder<TResult> ConvertToString(Func<TResult, string> convertFn)
        {
            _extensions.ConvertToStringComponentFn = () =>
            {
                return new ConvertToStringComponent<TResult>(convertFn);
            };

            return this;
        }

        public List<TResult> Prompt()
        {
            return Build().Prompt();
        }

        public PagedCheckboxBuilder<TResult> WithConfirmation()
        {
            _extensions.ConfirmComponentFn = () =>
            {
                return new ConfirmListComponent<List<TResult>, TResult>(_extensions.Convert);
            };

            return this;
        }

        public PagedCheckboxBuilder<TResult> WithDefaultValue(IEnumerable<TResult> defaultValues)
        {
            _extensions.DefaultValueComponentFn = () =>
            {
                return new DefaultValueComponent<List<TResult>>(defaultValues.ToList());
            };

            return this;
        }

        public PagedCheckboxBuilder<TResult> WithDefaultValue(List<TResult> defaultValues)
        {
            _extensions.DefaultValueComponentFn = () =>
            {
                return new DefaultSelectedValueComponent<TResult>(_choices, defaultValues);
            };

            return this;
        }

        public PagedCheckboxBuilder<TResult> WithDefaultValue(TResult defaultValues)
        {
            _extensions.DefaultValueComponentFn = () =>
            {
                return new DefaultSelectedValueComponent<TResult>(_choices, new List<TResult>() { defaultValues });
            };

            return this;
        }

        public PagedCheckboxBuilder<TResult> WithValidation(Func<List<TResult>, bool> fn, Func<List<TResult>, string> errorMessageFn)
        {
            _extensions.Validators.Add(fn, errorMessageFn);
            return this;
        }

        public PagedCheckboxBuilder<TResult> WithValidation(Func<List<TResult>, bool> fn, string errorMessage)
        {
            _extensions.Validators.Add(fn, errorMessage);
            return this;
        }
    }
}
