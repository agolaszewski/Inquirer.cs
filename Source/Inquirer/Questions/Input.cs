using System;
using InquirerCS.Interfaces;

namespace InquirerCS.Questions
{
    public class Input<TResult> : IQuestion<TResult>
    {
        private IConfirmComponent<TResult> _confirmComponent;

        private IDefaultValueComponent<TResult> _defaultValueComponent;

        private IDisplayQuestionComponent _displayQuestion;

        private IDisplayErrorComponent _errorComponent;

        private IWaitForInputComponent<string> _inputComponent;

        private IParseComponent<string, TResult> _parseComponent;

        private IValidateComponent<TResult> _validationResultComponent;

        private IValidateComponent<string> _validationValueComponent;

        public Input(
            IConfirmComponent<TResult> confirmComponent,
            IDisplayQuestionComponent displayQuestion,
            IWaitForInputComponent<string> inputComponent,
            IParseComponent<string, TResult> parseComponent,
            IValidateComponent<TResult> validationResultComponent,
            IValidateComponent<string> validationValueComponent,
            IDisplayErrorComponent errorComponent,
            IDefaultValueComponent<TResult> defaultComponent)
        {
            _confirmComponent = confirmComponent;
            _displayQuestion = displayQuestion;
            _inputComponent = inputComponent;
            _parseComponent = parseComponent;
            _validationResultComponent = validationResultComponent;
            _validationValueComponent = validationValueComponent;
            _errorComponent = errorComponent;
            _defaultValueComponent = defaultComponent;

            Console.CursorVisible = true;
        }

        public TResult Prompt()
        {
            _displayQuestion.Render();

            var value = _inputComponent.WaitForInput();
            if (string.IsNullOrWhiteSpace(value) && _defaultValueComponent.HasDefaultValue)
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

            var validationResult = _validationValueComponent.Run(value);
            if (validationResult.HasError)
            {
                _errorComponent.Render(validationResult.ErrorMessage);
                return Prompt();
            }

            TResult answer = _parseComponent.Parse(value);
            validationResult = _validationResultComponent.Run(answer);

            if (validationResult.HasError)
            {
                _errorComponent.Render(validationResult.ErrorMessage);
                return Prompt();
            }

            if (_confirmComponent.Confirm(answer))
            {
                return Prompt();
            }

            return answer;
        }
    }
}
