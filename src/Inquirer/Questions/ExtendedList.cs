using System;
using System.Collections.Generic;
using InquirerCS.Components;
using InquirerCS.Interfaces;

namespace InquirerCS.Questions
{
    public class ExtendedList<TResult> : IQuestion<TResult>
    {
        private Dictionary<ConsoleKey, TResult> _choices;

        private IConfirmComponent<TResult> _confirmComponent;

        private IDefaultValueComponent<TResult> _defaultValueComponent;

        private IRenderQuestionComponent _displayQuestion;

        private IDisplayErrorComponent _errorComponent;

        private IWaitForInputComponent<StringOrKey> _input;

        private IOnKey _onKey;

        private IParseComponent<ConsoleKey, TResult> _parseComponent;

        private IRenderChoices<TResult> _renderChoices;

        private IValidateComponent<ConsoleKey> _validationInputComponent;

        private IValidateComponent<TResult> _validationResultComponent;

        public ExtendedList(
            Dictionary<ConsoleKey, TResult> choices,
            IDefaultValueComponent<TResult> defaultValueComponent,
            IConfirmComponent<TResult> confirmComponent,
            IRenderQuestionComponent displayQuestion,
             IWaitForInputComponent<StringOrKey> inputComponent,
            IParseComponent<ConsoleKey, TResult> parseComponent,
            IRenderChoices<TResult> renderChoices,
            IValidateComponent<TResult> validationResultComponent,
            IValidateComponent<ConsoleKey> validationInputComponent,
            IDisplayErrorComponent errorComponent,
            IOnKey onKey)
        {
            _choices = choices;
            _confirmComponent = confirmComponent;
            _displayQuestion = displayQuestion;
            _input = inputComponent;
            _parseComponent = parseComponent;
            _renderChoices = renderChoices;
            _validationInputComponent = validationInputComponent;
            _validationResultComponent = validationResultComponent;
            _errorComponent = errorComponent;
            _defaultValueComponent = defaultValueComponent;
            _onKey = onKey;

            Console.CursorVisible = false;
        }

        public TResult Prompt()
        {
            _displayQuestion.Render();

            if (_choices.Count == 0)
            {
                _errorComponent.Render("No choices");
                var keyPressed = _input.WaitForInput().InterruptKey;
                _onKey.OnKey(keyPressed);
                return default(TResult);
            }

            _renderChoices.Render();

            var value = _input.WaitForInput().InterruptKey.Value;
            _onKey.OnKey(value);
            if (_onKey.IsInterrupted)
            {
                return default(TResult);
            }

            if (value == ConsoleKey.Enter && _defaultValueComponent.HasDefault)
            {
                if (_confirmComponent.Confirm(_defaultValueComponent.Value))
                {
                    return Prompt();
                }

                var defaultValueValidation = _validationResultComponent.Run(_defaultValueComponent.Value);

                if (defaultValueValidation.HasError)
                {
                    _errorComponent.Render(defaultValueValidation.ErrorMessage);
                    return Prompt();
                }

                return _defaultValueComponent.Value;
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
