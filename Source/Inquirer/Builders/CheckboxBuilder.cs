using System;
using System.Collections.Generic;
using System.Linq;
using InquirerCS.Components;
using InquirerCS.Interfaces;
using InquirerCS.Questions;

namespace InquirerCS.Builders
{
    public class CheckboxBuilder<TResult>
    {
        private IConfirmComponent<List<TResult>> _confirmComponent;
        private IConvertToStringComponent<TResult> _convertToStringComponent;
        private IDefaultValueComponent<List<TResult>> _defaultValueComponent;
        private IDisplayQuestionComponent _displayQuestionComponent;
        private IDisplayErrorComponent _errorComponent;
        private IWaitForInputComponent<ConsoleKey> _inputComponent;
        private IMessageComponent _messageComponent;
        private IParseComponent<List<Selectable<TResult>>, List<TResult>> _parseComponent;
        private IRenderChoices<TResult> _renderchoices;
        private IValidateComponent<List<TResult>> _validateComponent;

        private List<Selectable<TResult>> _selectedChoices;

        public CheckboxBuilder(string message, IEnumerable<TResult> choices)
        {
            _messageComponent = new MessageComponent(message);
            _selectedChoices = choices.Select(item => new Selectable<TResult>(false, item)).ToList();
        }

        public CheckboxBuilder<TResult> WithDefaultValue(IEnumerable<TResult> defaultValues)
        {
            _defaultValueComponent = new DefaultValueComponent<List<TResult>>(defaultValues.ToList());
            return this;
        }

        public CheckboxBuilder<TResult> WithDefaultValue(TResult defaultValues)
        {
            _defaultValueComponent = new DefaultValueComponent<List<TResult>>(new List<TResult>() { defaultValues });
            return this;
        }

        public CheckboxBuilder<TResult> WithDefaultValue(List<TResult> defaultValues)
        {
            _defaultValueComponent = new DefaultValueComponent<List<TResult>>(defaultValues);
            return this;
        }

        public CheckboxBuilder<TResult> ConvertToString(Func<TResult, string> convertFn)
        {
            _convertToStringComponent = new ConvertToStringComponent<TResult>(convertFn);
            return this;
        }

        public CheckboxBuilder<TResult> WithConfirmation()
        {
            _confirmComponent = new ConfirmListComponent<List<TResult>, TResult>(_convertToStringComponent);
            return this;
        }

        public List<TResult> Prompt()
        {
            _convertToStringComponent = _convertToStringComponent ?? new ConvertToStringComponent<TResult>();
            _defaultValueComponent = _defaultValueComponent ?? new DefaultValueComponent<List<TResult>>();
            _confirmComponent = _confirmComponent ?? new NoConfirmationComponent<List<TResult>>();

            _displayQuestionComponent = new DisplayListQuestion<List<TResult>, TResult>(_messageComponent, _convertToStringComponent, _defaultValueComponent);

            _inputComponent = new ReadConsoleKey();
            _parseComponent = new ParseSelectableListComponent<List<TResult>, TResult>(_selectedChoices);
            _renderchoices = new DisplaySelectableChoices<TResult>(_selectedChoices, _convertToStringComponent);
            _validateComponent = new ValidationComponent<List<TResult>>();
            _errorComponent = new DisplayErrorCompnent();

            return new Checkbox<List<TResult>, TResult>(_selectedChoices, _confirmComponent, _displayQuestionComponent, _inputComponent, _parseComponent, _renderchoices, _validateComponent, _errorComponent).Prompt();
        }
    }
}