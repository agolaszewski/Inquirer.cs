using System;
using System.Collections.Generic;
using System.Linq;
using InquirerCS.Components;
using InquirerCS.Interfaces;
using InquirerCS.Questions;

namespace InquirerCS.Builders
{
    public class RawListBuilder<TResult> : IBuilder<TResult> where TResult : IComparable
    {
        private IDisplayQuestionComponent _displayQuestionComponent;

        private IDisplayErrorComponent _errorDisplay;

        private IWaitForInputComponent<string> _inputComponent;

        private IParseComponent<string, TResult> _parseComponent;

        private IValidateComponent<string> _validationInputComponent;

        private List<TResult> _choices;

        private IRenderChoices<TResult> _displayChoices;

        private Extensions<TResult> _extensions = new Extensions<TResult>();

        private string _message;

        public RawListBuilder(string message, IEnumerable<TResult> choices)
        {
            _message = message;
            _choices = choices.ToList();
            _validationInputComponent = new ValidationComponent<string>();
        }

        public RawListBuilder<TResult> ConvertToString(Func<TResult, string> fn)
        {
            _extensions.ConvertToStringComponentFn = () =>
            {
                return new ConvertToStringComponent<TResult>(fn);
            };

            return this;
        }

        public PagedRawListBuilder<TResult> Page(int pageSize)
        {
            return new PagedRawListBuilder<TResult>(_message, _choices, pageSize, _extensions);
        }

        public TResult Prompt()
        {
            _extensions.Build();

            _displayQuestionComponent = new DisplayQuestion<TResult>(_message, _extensions.Convert, _extensions.Default);

            _inputComponent = new ReadStringComponent();

            _parseComponent = new ParseComponent<string, TResult>(value =>
            {
                return _choices[value.To<int>() - 1];
            });

            _displayChoices = new DisplaRawChoices<TResult>(_choices, _extensions.Convert);

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
                return $"Choosen number must be between 1 and {_choices.Count}";
            });

            _errorDisplay = new DisplayErrorCompnent();

            return new RawList<TResult>(_choices, _extensions.Confirm, _displayQuestionComponent, _inputComponent, _parseComponent, _displayChoices, _extensions.Validators, _validationInputComponent, _errorDisplay).Prompt();
        }

        public RawListBuilder<TResult> WithConfirmation()
        {
            _extensions.ConfirmComponentFn = () =>
            {
                return new ConfirmComponent<TResult>(_extensions.Convert);
            };

            return this;
        }

        public RawListBuilder<TResult> WithDefaultValue(TResult defaultValue)
        {
            _extensions.DefaultValueComponentFn = () =>
            {
                return new DefaultListValueComponent<TResult>(_choices, defaultValue);
            };

            return this;
        }

        public RawListBuilder<TResult> WithValidation(Func<TResult, bool> fn, Func<TResult, string> errorMessageFn)
        {
            _extensions.Validators.Add(fn, errorMessageFn);
            return this;
        }

        public RawListBuilder<TResult> WithValidation(Func<TResult, bool> fn, string errorMessage)
        {
            _extensions.Validators.Add(fn, errorMessage);
            return this;
        }
    }
}