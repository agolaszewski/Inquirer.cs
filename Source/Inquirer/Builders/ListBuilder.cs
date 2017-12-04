using System;
using System.Collections.Generic;
using System.Linq;
using InquirerCS.Components;
using InquirerCS.Interfaces;
using InquirerCS.Questions;

namespace InquirerCS.Builders
{
    public class ListBuilder<TResult> : IBuilder<TResult> where TResult : IComparable
    {
        private List<TResult> _choices;

        private IConfirmComponent<TResult> _confirmComponent;

        private Func<IConfirmComponent<TResult>> _confirmComponentFn = () => { return null; };

        private IConvertToStringComponent<TResult> _convertToString;

        private Func<IConvertToStringComponent<TResult>> _convertToStringComponentFn = () => { return null; };

        private IDefaultValueComponent<TResult> _defaultComponent;

        private Func<IDefaultValueComponent<TResult>> _defaultValueComponentFn = () => { return null; };

        private DisplayChoices<TResult> _displayChoices;

        private IDisplayQuestionComponent _displayQuestionComponent;

        private IDisplayErrorComponent _errorDisplay;

        private IWaitForInputComponent<ConsoleKey> _inputComponent;

        private string _message;

        private IParseComponent<int, TResult> _parseComponent;

        private IValidateComponent<TResult> _validationResultComponent;

        public ListBuilder(string message, IEnumerable<TResult> choices)
        {
            _message = message;
            _choices = choices.ToList();
        }

        public TResult Build()
        {
            _convertToString = new ConvertToStringComponent<TResult>();
            _confirmComponent = new NoConfirmationComponent<TResult>();
            _defaultComponent = new DefaultListValueComponent<TResult>();
            _displayQuestionComponent = new DisplayQuestion<TResult>(_message, _convertToString, _defaultComponent);
            _inputComponent = new ReadConsoleKey();
            _parseComponent = new ParseListComponent<TResult>(_choices);
            _displayChoices = new DisplayChoices<TResult>(_choices, _convertToString);
            _validationResultComponent = new ValidationComponent<TResult>();
            _errorDisplay = new DisplayErrorCompnent();

            return new Listing<TResult>(_choices, _confirmComponent, _displayQuestionComponent, _inputComponent, _parseComponent, _displayChoices, _validationResultComponent, _errorDisplay).Prompt();
        }
    }
}
