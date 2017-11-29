using System;
using System.Security;
using InquirerCS.Beta2.Interfaces;

namespace InquirerCS.Beta2.Questions
{
    public class Password : IQuestion<string>
    {
        private IConfirmComponent<string> _confirmComponent;

        private IDefaultValueComponent<string> _defaultComponent;

        private IDisplayQuestionComponent _displayQuestion;

        private IDisplayErrorComponent _errorComponent;

        private IWaitForInputComponent<string> _inputComponent;

        private IParseComponent<string, string> _parseComponent;

        private IValidateComponent<string> _validationResultComponent;

        private IValidateComponent<string> _validationValueComponent;

        public Password(
            IConfirmComponent<string> confirmComponent,
            IDisplayQuestionComponent displayQuestion,
            IWaitForInputComponent<string> inputComponent,
            IParseComponent<string, string> parseComponent,
            IValidateComponent<string> validationResultComponent,
            IValidateComponent<string> validationValueComponent,
            IDisplayErrorComponent errorComponent,
            IDefaultValueComponent<string> defaultComponent)
        {
            _confirmComponent = confirmComponent;
            _displayQuestion = displayQuestion;
            _inputComponent = inputComponent;
            _parseComponent = parseComponent;
            _validationResultComponent = validationResultComponent;
            _validationValueComponent = validationValueComponent;
            _errorComponent = errorComponent;
            _defaultComponent = defaultComponent;

            Console.CursorVisible = true;
        }

        public string Prompt()
        {
            _displayQuestion.Render();

            var value = _inputComponent.WaitForInput();
            if (string.IsNullOrWhiteSpace(value) && _defaultComponent.HasDefaultValue)
            {
                if (_confirmComponent.Confirm(_defaultComponent.DefaultValue))
                {
                    return Prompt();
                }

                var defaultValueValidation = _validationResultComponent.Run(_defaultComponent.DefaultValue);

                if (defaultValueValidation.HasError)
                {
                    _errorComponent.Render(defaultValueValidation.ErrorMessage);
                    return Prompt();
                }

                return _defaultComponent.DefaultValue;
            }

            var validationResult = _validationValueComponent.Run(value);
            if (validationResult.HasError)
            {
                _errorComponent.Render(validationResult.ErrorMessage);
                return Prompt();
            }

            string answer = _parseComponent.Parse(value);
            validationResult = _validationResultComponent.Run(answer);

            if (validationResult.HasError)
            {
                _errorComponent.Render(validationResult.ErrorMessage);
                return Prompt();
            }

            return answer;
        }
    }
}
