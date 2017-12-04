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
        }

        public ExtendedListBuilder<TResult> AddValidator(Func<TResult, bool> fn, Func<TResult, string> errorMessageFn)
        {
            _validationResultComponent.AddValidator(fn, errorMessageFn);
            return this;
        }

        public ExtendedListBuilder<TResult> AddValidator(Func<TResult, bool> fn, string errorMessage)
        {
            _validationResultComponent.AddValidator(fn, errorMessage);
            return this;
        }

        public override TResult Build()
        {
            _convertToString = new ConvertToStringComponent<TResult>();

            _defaultComponent = new DefaultValueComponent<TResult>();

            _displayQuestionComponent = new DisplayQuestion<TResult>(_msgComponent, _convertToString, _defaultComponent);
            _inputComponent = new ReadConsoleKey();
            _parseComponent = new ParseComponent<ConsoleKey, TResult>(value =>
            {
                return _choices[value];
            });
            _confirmComponent = new ConfirmComponent<TResult>(_convertToString);

            _validationInputComponent = new ValidationComponent<ConsoleKey>();

            _validationResultComponent = new ValidationComponent<TResult>();
            _errorDisplay = new DisplayErrorCompnent();
            _displayChoices = new DisplayExtendedChoices<TResult>(_choices, _convertToString);

            return new ExtendedList<TResult>(_choices, _confirmComponent, _displayQuestionComponent, _inputComponent, _parseComponent, _displayChoices, _validationResultComponent, _validationInputComponent, _errorDisplay).Prompt();
        }

        public ExtendedListBuilder<TResult> WithConfirmation()
        {
            _confirmComponentFn = () =>
            {
                return new ConfirmComponent<TResult>(_convertToString);
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
    }
}
