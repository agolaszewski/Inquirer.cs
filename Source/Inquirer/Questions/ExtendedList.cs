using System;
using System.Collections.Generic;
using InquirerCS.Interfaces;

namespace InquirerCS.Questions
{
    public class ExtendedList<TResult> : IQuestion<TResult>
    {
        private Dictionary<ConsoleKey, TResult> _choices;

        private IConfirmComponent<TResult> _confirmComponent;
        private IDefaultValueComponent<TResult> _defaultValueComponent;
        private IDisplayQuestionComponent _displayQuestion;

        private IDisplayErrorComponent _errorComponent;

        private IWaitForInputComponent<ConsoleKey> _inputComponent;

        private IParseComponent<ConsoleKey, TResult> _parseComponent;

        private IRenderChoices<TResult> _renderChoices;

        private IValidateComponent<ConsoleKey> _validationInputComponent;

        private IValidateComponent<TResult> _validationResultComponent;

        public ExtendedList(
            Dictionary<ConsoleKey, TResult> choices,
            IDefaultValueComponent<TResult> defaultValueComponent,
            IConfirmComponent<TResult> confirmComponent,
            IDisplayQuestionComponent displayQuestion,
            IWaitForInputComponent<ConsoleKey> inputComponent,
            IParseComponent<ConsoleKey, TResult> parseComponent,
            IRenderChoices<TResult> renderChoices,
            IValidateComponent<TResult> validationResultComponent,
            IValidateComponent<ConsoleKey> validationInputComponent,
            IDisplayErrorComponent errorComponent)
        {
            _choices = choices;
            _confirmComponent = confirmComponent;
            _displayQuestion = displayQuestion;
            _inputComponent = inputComponent;
            _parseComponent = parseComponent;
            _renderChoices = renderChoices;
            _validationInputComponent = validationInputComponent;
            _validationResultComponent = validationResultComponent;
            _errorComponent = errorComponent;
            _defaultValueComponent = defaultValueComponent;

            Console.CursorVisible = false;
        }

        public TResult Prompt()
        {
            _displayQuestion.Render();
            _renderChoices.Render();

            var value = _inputComponent.WaitForInput();
            if (value == ConsoleKey.Enter && _defaultValueComponent.HasDefaultValue)
            {
                if (_confirmComponent.Confirm(_defaultValueComponent.DefaultValue))
                {
                    return Prompt();
                }

                var defaultValueValidation = _validationResultComponent.Run(_defaultValueComponent.DefaultValue);

                if (defaultValueValidation.HasError)
                {
                    _errorComponent.Render(defaultValueValidation.ErrorMessage);
                    return Prompt();
                }

                return _defaultValueComponent.DefaultValue;
            }

            var validationResult = _validationInputComponent.Run(value);
            if (validationResult.HasError)
            {
                _errorComponent.Render(validationResult.ErrorMessage);
                return Prompt();
            }

            TResult result = _parseComponent.Parse(value);
            validationResult = _validationResultComponent.Run(result);
            if (validationResult.HasError)
            {
                _errorComponent.Render(validationResult.ErrorMessage);
                return Prompt();
            }

            if (_confirmComponent.Confirm(result))
            {
                return Prompt();
            }

            return result;
        }
    }
}
