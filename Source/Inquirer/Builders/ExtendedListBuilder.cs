using System;
using System.Collections.Generic;
using System.Linq;
using InquirerCS.Components;
using InquirerCS.Questions;

namespace InquirerCS.Builders
{
    public class ExtendedListBuilder<TResult> : Builder<ConsoleKey, TResult>
    {
        private Dictionary<ConsoleKey, TResult> _choices;

        private DisplayExtendedChoices<TResult> _displayChoices;

        private string _message;

        public ExtendedListBuilder(string message, IDictionary<ConsoleKey, TResult> choices)
        {
            _message = message;
            _choices = choices.ToDictionary(k => k.Key, v => v.Value);
            _validationResultComponent = new ValidationComponent<TResult>();
            _validationInputComponent = new ValidationComponent<ConsoleKey>();
        }

        public override TResult Prompt()
        {
            _convertToStringComponent = _convertToStringComponentFn() ?? new ConvertToStringComponent<TResult>();
            _defaultValueComponent = _defaultValueComponentFn() ?? new DefaultValueComponent<TResult>();
            _confirmComponent = _confirmComponentFn() ?? new NoConfirmationComponent<TResult>();

            _displayQuestionComponent = new DisplayQuestion<TResult>(_message, _convertToStringComponent, _defaultValueComponent);
            _inputComponent = new ReadConsoleKey();
            _parseComponent = new ParseComponent<ConsoleKey, TResult>(value =>
            {
                return _choices[value];
            });
            _confirmComponent = new ConfirmComponent<TResult>(_convertToStringComponent);

            _errorDisplay = new DisplayErrorCompnent();
            _displayChoices = new DisplayExtendedChoices<TResult>(_choices, _convertToStringComponent);

            _validationInputComponent.Add(
            value =>
            {
                return _choices.Keys.Any(p => p == value);
            },
            value =>
            {
                string keys = " Press : ";
                foreach (var key in _choices.Keys)
                {
                    keys += $"[{(char)key}] ";
                }

                return keys;
            });

            return new ExtendedList<TResult>(_choices, _defaultValueComponent, _confirmComponent, _displayQuestionComponent, _inputComponent, _parseComponent, _displayChoices, _validationResultComponent, _validationInputComponent, _errorDisplay).Prompt();
        }

        public ExtendedListBuilder<TResult> ConvertToString(Func<TResult, string> fn)
        {
            _convertToStringComponentFn = () =>
            {
                return new ConvertToStringComponent<TResult>(fn);
            };

            return this;
        }

        public ExtendedListBuilder<TResult> WithConfirmation()
        {
            _confirmComponentFn = () =>
            {
                return new ConfirmComponent<TResult>(_convertToStringComponent);
            };

            return this;
        }

        public ExtendedListBuilder<TResult> WithDefaultValue(TResult defaultValues)
        {
            _defaultValueComponentFn = () =>
            {
                return new DefaultValueComponent<TResult>(defaultValues);
            };

            return this;
        }

        public ExtendedListBuilder<TResult> WithValidation(Func<TResult, bool> fn, Func<TResult, string> errorMessageFn)
        {
            _validationResultComponent.Add(fn, errorMessageFn);
            return this;
        }

        public ExtendedListBuilder<TResult> WithValidation(Func<TResult, bool> fn, string errorMessage)
        {
            _validationResultComponent.Add(fn, errorMessage);
            return this;
        }
    }
}