using System;
using InquirerCS.Components;
using InquirerCS.Interfaces;

namespace InquirerCS.Questions
{
    public class Input<TResult> : IQuestion<TResult>
    {
        public IWaitForInputComponent<StringOrKey> Reader;

        private IConfirmComponent<TResult> _confirmComponent;

        private IDefaultValueComponent<TResult> _defaultValueComponent;

        private IRenderQuestionComponent _displayQuestion;

        private IDisplayErrorComponent _errorComponent;

        private IOnKey _onKey;

        private IParseComponent<string, TResult> _parseComponent;

        private IValidateComponent<TResult> _validationResultComponent;

        private IValidateComponent<string> _validationValueComponent;

        public Input(
            IConfirmComponent<TResult> confirmComponent,
            IRenderQuestionComponent displayQuestion,
            IWaitForInputComponent<StringOrKey> inputComponent,
            IParseComponent<string, TResult> parseComponent,
            IValidateComponent<TResult> validationResultComponent,
            IValidateComponent<string> validationValueComponent,
            IDisplayErrorComponent errorComponent,
            IDefaultValueComponent<TResult> defaultComponent,
            IOnKey onKey)
        {
            _confirmComponent = confirmComponent;
            _displayQuestion = displayQuestion;
            Reader = inputComponent;
            _parseComponent = parseComponent;
            _validationResultComponent = validationResultComponent;
            _validationValueComponent = validationValueComponent;
            _errorComponent = errorComponent;
            _defaultValueComponent = defaultComponent;
            _onKey = onKey;

            Console.CursorVisible = true;
        }

        public TResult Prompt()
        {
            _displayQuestion.Render();

            var value = Reader.WaitForInput();
            _onKey.OnKey(value.InterruptKey);
            if (_onKey.IsInterrupted)
            {
                return default(TResult);
            }

            if (string.IsNullOrWhiteSpace(value.Value) && _defaultValueComponent.HasDefault)
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

            var validationResult = _validationValueComponent.Run(value.Value);
            if (validationResult.HasError)
            {
                _errorComponent.Render(validationResult.ErrorMessage);
                return Prompt();
            }

            TResult answer = _parseComponent.Parse(value.Value);
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
