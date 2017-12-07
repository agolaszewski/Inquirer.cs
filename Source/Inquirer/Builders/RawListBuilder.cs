using System;
using System.Collections.Generic;
using System.Linq;
using InquirerCS.Components;
using InquirerCS.Interfaces;
using InquirerCS.Questions;

namespace InquirerCS.Builders
{
    public class RawListBuilder<TResult> : Builder<string, TResult> where TResult : IComparable
    {
        private List<TResult> _choices;

        private IRenderChoices<TResult> _displayChoices;

        private string _message;

        public RawListBuilder(string message, IEnumerable<TResult> choices)
        {
            _message = message;
            _choices = choices.ToList();
            _validationResultComponent = new ValidationComponent<TResult>();
            _validationInputComponent = new ValidationComponent<string>();
        }

        public RawListBuilder<TResult> ConvertToString(Func<TResult, string> fn)
        {
            _convertToStringComponentFn = () =>
            {
                return new ConvertToStringComponent<TResult>(fn);
            };

            return this;
        }

        public override TResult Prompt()
        {
            _convertToStringComponent = _convertToStringComponentFn() ?? new ConvertToStringComponent<TResult>();
            _defaultValueComponent = _defaultValueComponentFn() ?? new DefaultValueComponent<TResult>();
            _confirmComponent = _confirmComponentFn() ?? new NoConfirmationComponent<TResult>();

            _displayQuestionComponent = new DisplayQuestion<TResult>(_message, _convertToStringComponent, _defaultValueComponent);
            _inputComponent = new ReadStringComponent();

            _parseComponent = new ParseComponent<string, TResult>(value =>
            {
                return _choices[value.To<int>() - 1];
            });

            _displayChoices = new DisplaRawChoices<TResult>(_choices, _convertToStringComponent);

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
                return $"Choosen number must be between 1 and {_choices.Count}";
            });

            var errorDisplay = new DisplayErrorCompnent();

            return new RawList<TResult>(_choices, _confirmComponent, _displayQuestionComponent, _inputComponent, _parseComponent, _displayChoices, _validationResultComponent, _validationInputComponent, errorDisplay).Prompt();
        }

        public RawListBuilder<TResult> WithConfirmation()
        {
            _confirmComponentFn = () =>
            {
                return new ConfirmComponent<TResult>(_convertToStringComponentFn());
            };

            return this;
        }

        public RawListBuilder<TResult> WithDefaultValue(TResult defaultValue)
        {
            _defaultValueComponentFn = () =>
            {
                return new DefaultListValueComponent<TResult>(_choices, defaultValue);
            };

            return this;
        }

        public RawListBuilder<TResult> WithValidation(Func<TResult, bool> fn, Func<TResult, string> errorMessageFn)
        {
            _validationResultComponent.Add(fn, errorMessageFn);
            return this;
        }

        public RawListBuilder<TResult> WithValidation(Func<TResult, bool> fn, string errorMessage)
        {
            _validationResultComponent.Add(fn, errorMessage);
            return this;
        }

        public PagedRawListBuilder<TResult> Page(int pageSize)
        {
            return new PagedRawListBuilder<TResult>(_message, _choices, pageSize, _convertToStringComponentFn, _confirmComponentFn, _defaultValueComponentFn);
        }
    }
}
