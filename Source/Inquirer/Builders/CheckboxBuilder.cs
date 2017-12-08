using System;
using System.Collections.Generic;
using System.Linq;
using InquirerCS.Components;
using InquirerCS.Interfaces;
using InquirerCS.Questions;

namespace InquirerCS.Builders
{
    public class CheckboxBuilder<TResult> : IBuilder<Checkbox<List<TResult>, TResult>, List<TResult>> where TResult : IComparable
    {
        private IEnumerable<TResult> _choices;

        private IDisplayQuestionComponent _displayQuestionComponent;

        private IDisplayErrorComponent _errorComponent;

        private ExtensionsCheckbox<TResult> _extensions;

        private IWaitForInputComponent<StringOrKey> _inputComponent;

        private string _message;

        private IOnKey _onKey;

        private IParseComponent<List<Selectable<TResult>>, List<TResult>> _parseComponent;

        private IRenderChoices<TResult> _renderchoices;

        private List<Selectable<TResult>> _selectedChoices;

        public CheckboxBuilder(string message, IEnumerable<TResult> choices)
        {
            _message = message;
            _choices = choices;
            _selectedChoices = choices.Select(item => new Selectable<TResult>(false, item)).ToList();
            _extensions = new ExtensionsCheckbox<TResult>();
        }

        public Checkbox<List<TResult>, TResult> Build()
        {
            _extensions.Build();

            _displayQuestionComponent = new DisplayListQuestion<List<TResult>, TResult>(_message, _extensions.Convert, _extensions.Default);

            _inputComponent = new ReadConsoleKey();
            _parseComponent = new ParseSelectableListComponent<List<TResult>, TResult>(_selectedChoices);
            _renderchoices = new DisplaySelectableChoices<TResult>(_selectedChoices, _extensions.Convert);
            _errorComponent = new DisplayErrorCompnent();

            return new Checkbox<List<TResult>, TResult>(_selectedChoices, _extensions.Confirm, _displayQuestionComponent, _inputComponent, _parseComponent, _renderchoices, _extensions.Validators, _errorComponent, _onKey);
        }

        public CheckboxBuilder<TResult> ConvertToString(Func<TResult, string> convertFn)
        {
            _extensions.ConvertToStringComponentFn = () =>
            {
                return new ConvertToStringComponent<TResult>(convertFn);
            };

            return this;
        }

        public PagedCheckboxBuilder<TResult> Page(int pageSize)
        {
            return new PagedCheckboxBuilder<TResult>(_message, _selectedChoices, pageSize, _extensions);
        }

        public List<TResult> Prompt()
        {
            return Build().Prompt();
        }

        public CheckboxBuilder<TResult> WithConfirmation()
        {
            _extensions.ConfirmComponentFn = () =>
            {
                return new ConfirmListComponent<List<TResult>, TResult>(_extensions.Convert);
            };

            return this;
        }

        public CheckboxBuilder<TResult> WithDefaultValue(IEnumerable<TResult> defaultValues)
        {
            _extensions.DefaultValueComponentFn = () =>
            {
                return new DefaultValueComponent<List<TResult>>(defaultValues.ToList());
            };

            return this;
        }

        public CheckboxBuilder<TResult> WithDefaultValue(List<TResult> defaultValues)
        {
            _extensions.DefaultValueComponentFn = () =>
            {
                return new DefaultSelectedValueComponent<TResult>(_selectedChoices, defaultValues);
            };

            return this;
        }

        public CheckboxBuilder<TResult> WithDefaultValue(TResult defaultValues)
        {
            _extensions.DefaultValueComponentFn = () =>
            {
                return new DefaultSelectedValueComponent<TResult>(_selectedChoices, new List<TResult>() { defaultValues });
            };

            return this;
        }

        public CheckboxBuilder<TResult> WithValidation(Func<List<TResult>, bool> fn, Func<List<TResult>, string> errorMessageFn)
        {
            _extensions.Validators.Add(fn, errorMessageFn);
            return this;
        }

        public CheckboxBuilder<TResult> WithValidation(Func<List<TResult>, bool> fn, string errorMessage)
        {
            _extensions.Validators.Add(fn, errorMessage);
            return this;
        }
    }
}
