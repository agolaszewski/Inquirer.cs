using System;
using System.Collections.Generic;
using System.Linq;
using InquirerCS.Components;
using InquirerCS.Interfaces;
using InquirerCS.Questions;

namespace InquirerCS.Builders
{
    public class PagedCheckboxBuilder<TResult> : IBuilder<List<TResult>> where TResult : IComparable
    {
        private Func<IConfirmComponent<List<TResult>>> _confirmComponentFn = () => { return null; };

        private Func<IConvertToStringComponent<TResult>> _convertToStringComponentFn = () => { return null; };

        private Func<IDefaultValueComponent<List<TResult>>> _defaultValueComponentFn = () => { return null; };

        private List<Selectable<TResult>> _choices;

        private IConfirmComponent<List<TResult>> _confirmComponent;

        private IConvertToStringComponent<TResult> _convertToStringComponent;

        private IDefaultValueComponent<List<TResult>> _defaultValueComponent;

        private IDisplayQuestionComponent _displayQuestionComponent;

        private IDisplayErrorComponent _errorComponent;

        private IWaitForInputComponent<ConsoleKey> _inputComponent;

        private string _message;

        private IPagingComponent<Selectable<TResult>> _pagingComponent;

        private IParseComponent<Dictionary<int, List<Selectable<TResult>>>, List<TResult>> _parseComponent;

        private IRenderChoices<TResult> _renderchoices;

        private IValidateComponent<List<TResult>> _validationResultComponent;
        private int _pageSize;

        public PagedCheckboxBuilder(string message, List<Selectable<TResult>> choices, int pageSize, Func<IConvertToStringComponent<TResult>> convertToStringComponentFn, Func<IConfirmComponent<List<TResult>>> confirmComponentFn, Func<IDefaultValueComponent<List<TResult>>> defaultValueComponentFn)
        {
            _choices = choices;
            _pageSize = pageSize;

            _message = message;

            _convertToStringComponentFn = convertToStringComponentFn;
            _confirmComponentFn = confirmComponentFn;
            _defaultValueComponentFn = defaultValueComponentFn;

            _validationResultComponent = new ValidationComponent<List<TResult>>();
        }

        public PagedCheckboxBuilder<TResult> ConvertToString(Func<TResult, string> convertFn)
        {
            _convertToStringComponentFn = () =>
            {
                return new ConvertToStringComponent<TResult>(convertFn);
            };

            return this;
        }

        public List<TResult> Prompt()
        {
            _convertToStringComponent = _convertToStringComponentFn() ?? new ConvertToStringComponent<TResult>();
            _defaultValueComponent = _defaultValueComponentFn() ?? new DefaultValueComponent<List<TResult>>();
            _confirmComponent = _confirmComponentFn() ?? new NoConfirmationComponent<List<TResult>>();

            _pagingComponent = new PagingComponent<Selectable<TResult>>(_choices, _pageSize);

            _displayQuestionComponent = new DisplayListQuestion<List<TResult>, TResult>(_message, _convertToStringComponent, _defaultValueComponent);

            _inputComponent = new ReadConsoleKey();
            _parseComponent = new ParseSelectablePagedListComponent<List<TResult>, TResult>(_pagingComponent.PagedChoices);
            _renderchoices = new DisplaySelectablePagedChoices<TResult>(_pagingComponent, _convertToStringComponent);
            _errorComponent = new DisplayErrorCompnent();

            return new PagedCheckbox<List<TResult>, TResult>(_pagingComponent, _confirmComponent, _displayQuestionComponent, _inputComponent, _parseComponent, _renderchoices, _validationResultComponent, _errorComponent).Prompt();
        }

        public PagedCheckboxBuilder<TResult> WithConfirmation()
        {
            _confirmComponentFn = () =>
            {
                return new ConfirmListComponent<List<TResult>, TResult>(_convertToStringComponentFn());
            };

            return this;
        }

        public PagedCheckboxBuilder<TResult> WithDefaultValue(IEnumerable<TResult> defaultValues)
        {
            _defaultValueComponentFn = () =>
            {
                return new DefaultValueComponent<List<TResult>>(defaultValues.ToList());
            };

            return this;
        }

        public PagedCheckboxBuilder<TResult> WithDefaultValue(List<TResult> defaultValues)
        {
            _defaultValueComponentFn = () =>
            {
                return new DefaultSelectedValueComponent<TResult>(_choices, defaultValues);
            };

            return this;
        }

        public PagedCheckboxBuilder<TResult> WithDefaultValue(TResult defaultValues)
        {
            _defaultValueComponentFn = () =>
            {
                return new DefaultSelectedValueComponent<TResult>(_choices, new List<TResult>() { defaultValues });
            };

            return this;
        }

        public PagedCheckboxBuilder<TResult> WithValidation(Func<List<TResult>, bool> fn, Func<List<TResult>, string> errorMessageFn)
        {
            _validationResultComponent.Add(fn, errorMessageFn);
            return this;
        }

        public PagedCheckboxBuilder<TResult> WithValidation(Func<List<TResult>, bool> fn, string errorMessage)
        {
            _validationResultComponent.Add(fn, errorMessage);
            return this;
        }
    }
}