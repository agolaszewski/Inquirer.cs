namespace InquirerCS.Beta
{
    public class QuestionInput<TResult> : QuestionBase<TResult>
    {
        private IConfirmComponent<TResult> _confirmComponent;

        private IDisplayQuestionComponent _displayQuestion;

        private IParseComponent<string, TResult> _parseComponent;

        private IReadInputComponent<string> _readComponent;

        private IUISystem _uiComponent;

        private IValidateComponent<string> _validateInput;

        private IValidateComponent<TResult> _validateResult;

        public QuestionInput(
            IUISystem uiComponent,
            IReadInputComponent<string> readComponent,
            IValidateComponent<string> validateInput,
            IValidateComponent<TResult> validateResult,
            IParseComponent<string, TResult> parseComponent,
            IConfirmComponent<TResult> confirmComponent,
            IDisplayQuestionComponent displayQuestion)
        {
            _uiComponent = uiComponent;
            _readComponent = readComponent;
            _validateInput = validateInput;
            _validateResult = validateResult;
            _parseComponent = parseComponent;
            _confirmComponent = confirmComponent;
            _displayQuestion = displayQuestion;
        }

        public override TResult Prompt()
        {
            _uiComponent.Render(_displayQuestion);

            string value = _readComponent.WaitForInput();

            IValidationResultComponent validationResult = _validateInput.Run(value);
            _uiComponent.Render(validationResult);

            var result = _parseComponent.Parse(value);

            validationResult = _validateResult.Run(result);
            _uiComponent.Render(validationResult);

            var confirmEntity = new ConfirmEntity<TResult>(result, _confirmComponent);

            if (_uiComponent.Render())
            {
                return result;
            }

            ////return Prompt();
            return default(TResult);
        }
    }
}