using System;
using System.Collections.Generic;
using InquirerCS.Components;
using InquirerCS.Interfaces;

namespace InquirerCS.Questions
{
    public class RawList<TResult> : IQuestion<TResult>
    {
        private List<TResult> _choices;

        private IConfirmComponent<TResult> _confirmComponent;

        private IConsole _console;

        private IRenderQuestionComponent _displayQuestion;

        private IDisplayErrorComponent _errorComponent;

        private IWaitForInputComponent<StringOrKey> _inputComponent;

        private IOnKey _onKey;

        private IParseComponent<string, TResult> _parseComponent;

        private IRenderChoices<TResult> _renderChoices;

        private IValidateComponent<string> _validationInputComponent;

        private IValidateComponent<TResult> _validationResultComponent;

        public RawList(
            List<TResult> choices,
            IConfirmComponent<TResult> confirmComponent,
            IRenderQuestionComponent displayQuestion,
            IWaitForInputComponent<StringOrKey> inputComponent,
            IParseComponent<string, TResult> parseComponent,
            IRenderChoices<TResult> renderChoices,
            IValidateComponent<TResult> validationResultComponent,
            IValidateComponent<string> validationInputComponent,
            IDisplayErrorComponent errorComponent,
            IOnKey onKey,
            IConsole console)
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
            _onKey = onKey;
            _console = console;

            Console.CursorVisible = false;
        }

        public TResult Prompt()
        {
            _displayQuestion.Render();

            if (_choices.Count == 0)
            {
                _errorComponent.Render("No choices");
                var keyPressed = _inputComponent.WaitForInput().InterruptKey;
                _onKey.OnKey(keyPressed);
                return default(TResult);
            }

            _renderChoices.Render();

            _console.WriteLine();
            _console.Write("Answer: ");
            var value = _inputComponent.WaitForInput();
            _onKey.OnKey(value.InterruptKey);
            if (_onKey.IsInterrupted)
            {
                return default(TResult);
            }

            var validationResult = _validationInputComponent.Run(value.Value);
            if (validationResult.HasError)
            {
                _errorComponent.Render(validationResult.ErrorMessage);
                return Prompt();
            }

            TResult result = _parseComponent.Parse(value.Value);
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
