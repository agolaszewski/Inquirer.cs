namespace InquirerCS.Beta
{
    public class QuestionInput<TResult> : QuestionBase<TResult>
    {
        private IConfirmComponent<TResult> _confirmComponent;

        private IParseComponent<string, TResult> _parseComponent;

        private IReadInputComponent<string> _readComponent;

        private IUIComponent _uiComponent;

        private IValidateComponent<string> _validateInput;

        private IValidateComponent<TResult> _validateResult;

        public QuestionInput(
            IUIComponent uiComponent,
            IReadInputComponent<string> readComponent,
            IValidateComponent<string> validateInput,
            IValidateComponent<TResult> validateResult,
            IParseComponent<string, TResult> parseComponent,
            IConfirmComponent<TResult> confirmComponent)
        {
            _uiComponent = uiComponent;
            _readComponent = readComponent;
            _validateInput = validateInput;
            _validateResult = validateResult;
            _parseComponent = parseComponent;
            _confirmComponent = confirmComponent;
        }

        public override TResult Prompt()
        {
            _uiComponent.Render();

            var value = _readComponent.WaitForInput();

            _validateInput.Run(value);
            _uiComponent.Render(_validateInput);

            var result = _parseComponent.Parse(value);

            _validateResult.Run(result);
            _uiComponent.Render(_validateResult);

            if (_confirmComponent.Run(result))
            {
                return result;
            }

            return Prompt();
        }
    }
}
