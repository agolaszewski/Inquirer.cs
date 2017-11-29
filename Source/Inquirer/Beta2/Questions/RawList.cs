using System;
using InquirerCS.Beta2.Interfaces;

namespace InquirerCS.Beta2.Questions
{
    public class RawList<TResult> : IQuestion<TResult>
    {
        private IChoicesComponent<TResult> _choicesComponent;

        private IConfirmComponent<TResult> _confirmComponent;

        private IDisplayQuestionComponent _displayQuestion;

        private IWaitForInputComponent<string> _inputComponent;

        private IParseComponent<string, TResult> _parseComponent;

        private IRenderChoicesComponent<TResult> _renderChoices;

        private IValidateComponent<TResult> _validationResultComponent;

        private IValidateComponent<string> _validationInputComponent;

        private IDisplayErrorComponent _errorComponent;

        public RawList(
            IChoicesComponent<TResult> choicesComponent,
            IConfirmComponent<TResult> confirmComponent,
            IDisplayQuestionComponent displayQuestion,
            IWaitForInputComponent<string> inputComponent,
            IParseComponent<string, TResult> parseComponent,
            IRenderChoicesComponent<TResult> renderChoices,
            IValidateComponent<TResult> validationResultComponent,
            IValidateComponent<string> validationInputComponent,
            IDisplayErrorComponent errorComponent)
        {
            _choicesComponent = choicesComponent;
            _confirmComponent = confirmComponent;
            _displayQuestion = displayQuestion;
            _inputComponent = inputComponent;
            _parseComponent = parseComponent;
            _renderChoices = renderChoices;
            _validationInputComponent = validationInputComponent;
            _validationResultComponent = validationResultComponent;
            _errorComponent = errorComponent;

            Console.CursorVisible = false;
        }

        public TResult Prompt()
        {
            _displayQuestion.Render();
            _renderChoices.Render();

            var value = _inputComponent.WaitForInput();

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