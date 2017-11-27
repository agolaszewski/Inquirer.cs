using System;
using InquirerCS.Beta2.Interfaces;

namespace InquirerCS.Beta2.Questions
{
    public class QuestionInput<TResult> : IQuestion<TResult>
    {
        private IConfirmComponent<TResult> _confirmComponent;

        private IDefaultValueComponent<TResult> _defaultComponent;

        private IDisplayQuestionComponent _displayQuestion;

        private IDisplayErrorComponent _errorComponent;

        private IWaitForInputComponent<string> _inputComponent;

        private IParseComponent<string, TResult> _parseComponent;

        private IRenderChoicesComponent<TResult> _renderChoices;

        private IValidateComponent<TResult> _validationComponent;

        public QuestionInput(
            IConfirmComponent<TResult> confirmComponent,
            IDisplayQuestionComponent displayQuestion,
            IWaitForInputComponent<string> inputComponent,
            IParseComponent<string, TResult> parseComponent,
            IValidateComponent<TResult> validationComponent,
            IDisplayErrorComponent errorComponent,
            IDefaultValueComponent<TResult> defaultComponent)
        {
            _confirmComponent = confirmComponent;
            _displayQuestion = displayQuestion;
            _inputComponent = inputComponent;
            _parseComponent = parseComponent;
            _validationComponent = validationComponent;
            _errorComponent = errorComponent;
            _defaultComponent = defaultComponent;

            Console.CursorVisible = true;
        }

        public TResult Prompt()
        {
            _displayQuestion.Render();

            var value = _inputComponent.WaitForInput();
            if (string.IsNullOrWhiteSpace(value) && _defaultComponent.HasDefaultValue)
            {
                if (_confirmComponent.Confirm(_defaultComponent.DefaultValue))
                {
                    return Prompt();
                }

                return _defaultComponent.DefaultValue;
            }

            TResult answer = _parseComponent.Parse(value);
            var validationResult = _validationComponent.Run(answer);

            if (validationResult.HasError)
            {
                _errorComponent.Render(validationResult.ErrorMessage);
                return Prompt();
            }

            return answer;
        }
    }
}