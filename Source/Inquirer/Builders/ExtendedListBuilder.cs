using System;
using System.Collections.Generic;
using System.Linq;
using InquirerCS.Components;
using InquirerCS.Interfaces;
using InquirerCS.Questions;

namespace InquirerCS.Builders
{
    public class ExtendedListBuilder<TResult> where TResult : IComparable
    {
        private Dictionary<ConsoleKey, TResult> _choicesDictionary;
        private IConfirmComponent<TResult> _confirmComponent;
        private IConvertToStringComponent<TResult> _convertToString;
        private IDefaultValueComponent<TResult> _defaultComponent;
        private IRenderChoices<TResult> _displayChoices;
        private IDisplayQuestionComponent _displayQuestionComponent;
        private IDisplayErrorComponent _errorDisplay;
        private IWaitForInputComponent<ConsoleKey> _inputComponent;
        private IMessageComponent _msgComponent;
        private IParseComponent<ConsoleKey, TResult> _parseComponent;
        private IValidateComponent<ConsoleKey> _validationInputComponent;
        private IValidateComponent<TResult> _validationResultComponent;

        private Func<IConfirmComponent<TResult>> _confirmComponentFn;
        private Func<IDefaultValueComponent<TResult>> _defaultValueComponentFn;
        private Func<IConvertToStringComponent<TResult>> _convertToStringFn;

        public ExtendedListBuilder(string message, IDictionary<ConsoleKey, TResult> choices)
        {
            _choicesDictionary = choices.ToDictionary(k => k.Key, v => v.Value);
            _convertToString = new ConvertToStringComponent<TResult>();
            _msgComponent = new MessageComponent(message);

            _defaultComponent = new DefaultValueComponent<TResult>();

            _displayQuestionComponent = new DisplayQuestion<TResult>(_msgComponent, _convertToString, _defaultComponent);
            _inputComponent = new ReadConsoleKey();

            _parseComponent = new ParseComponent<ConsoleKey, TResult>(value =>
            {
                return choices[value];
            });

            _confirmComponent = new ConfirmComponent<TResult>(_convertToString);

            _validationInputComponent = new ValidationComponent<ConsoleKey>();
            _validationResultComponent = new ValidationComponent<TResult>();
            _errorDisplay = new DisplayErrorCompnent();
            _displayChoices = new DisplayExtendedChoices<TResult>(_choicesDictionary, _convertToString);
        }

        public ExtendedListBuilder<TResult> WithDefaultValue(TResult defaultValues)
        {
            _defaultValueComponentFn = () =>
            {
                return new DefaultDictionaryValueComponent<ConsoleKey, TResult>(_choicesDictionary, defaultValues);
            };

            return this;
        }

        public ExtendedListBuilder<TResult> ConvertToString(Func<TResult, string> convertFn)
        {
            _convertToStringFn = () =>
            {
                return new ConvertToStringComponent<TResult>(convertFn);
            };

            return this;
        }

        public ExtendedListBuilder<TResult> WithConfirmation()
        {
            _confirmComponentFn = () =>
            {
                return new ConfirmComponent<TResult>(_convertToString);
            };

            return this;
        }

        public TResult Prompt()
        {
            return new ExtendedList<TResult>(_choicesDictionary, _confirmComponent, _displayQuestionComponent, _inputComponent, _parseComponent, _displayChoices, _validationResultComponent, _validationInputComponent, _errorDisplay).Prompt();
        }
    }
}