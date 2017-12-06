﻿using System;
using InquirerCS.Components;
using InquirerCS.Questions;

namespace InquirerCS.Builders
{
    public class InputStructBuilder<TResult> : Builder<string, TResult> where TResult : struct
    {
        private string _message;

        public InputStructBuilder(string message)
        {
            _message = message;
        }

        public override TResult Prompt()
        {
            _convertToStringComponent = new ConvertToStringComponent<TResult>();

            _confirmComponent = new NoConfirmationComponent<TResult>();
            _defaultValueComponent = new DefaultValueComponent<TResult>();

            _displayQuestionComponent = new DisplayQuestion<TResult>(_message, _convertToStringComponent, _defaultValueComponent);
            _inputComponent = new ReadStringComponent();
            _parseComponent = new ParseComponent<string, TResult>(value =>
            {
                return value.To<TResult>();
            });

            var validationInputComponent = new ValidationComponent<string>();
            validationInputComponent.Add(value => { return string.IsNullOrEmpty(value) == false || _defaultValueComponent.HasDefaultValue; }, "Empty line");
            validationInputComponent.Add(value => { return value.ToN<TResult>().HasValue; }, value => { return $"Cannot parse {value} to {typeof(TResult)}"; });

            var validationResultComponent = new ValidationComponent<TResult>();
            var errorDisplay = new DisplayErrorCompnent();

            return new Input<TResult>(_confirmComponent, _displayQuestionComponent, _inputComponent, _parseComponent, validationResultComponent, validationInputComponent, errorDisplay, _defaultValueComponent).Prompt();
        }

        public InputStructBuilder<TResult> WithConfirmation()
        {
            _confirmComponentFn = () =>
            {
                return new ConfirmComponent<TResult>(_convertToStringComponent);
            };

            return this;
        }

        public InputStructBuilder<TResult> WithDefaultValue(TResult defaultValues)
        {
            _defaultValueComponentFn = () =>
            {
                return new DefaultValueComponent<TResult>(defaultValues);
            };

            return this;
        }

        public InputStructBuilder<TResult> WithValidation(Func<TResult, bool> fn, Func<TResult, string> errorMessageFn)
        {
            _validationResultComponent.Add(fn, errorMessageFn);
            return this;
        }

        public InputStructBuilder<TResult> WithValidation(Func<TResult, bool> fn, string errorMessage)
        {
            _validationResultComponent.Add(fn, errorMessage);
            return this;
        }
    }
}