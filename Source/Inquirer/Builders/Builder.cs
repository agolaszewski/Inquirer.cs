using System;
using InquirerCS.Interfaces;

namespace InquirerCS.Builders
{
    public abstract class Builder<TInput, TResult>
    {
        protected IConfirmComponent<TResult> _confirmComponent;
        protected IConvertToStringComponent<TResult> _convertToString;
        protected IDefaultValueComponent<TResult> _defaultComponent;
        protected IDisplayQuestionComponent _displayQuestionComponent;
        protected IDisplayErrorComponent _errorDisplay;
        protected IWaitForInputComponent<TInput> _inputComponent;
        protected string _msgComponent;
        protected IParseComponent<TInput, TResult> _parseComponent;
        protected IValidateComponent<TInput> _validationInputComponent;
        protected IValidateComponent<TResult> _validationResultComponent;

        protected Func<IConfirmComponent<TResult>> _confirmComponentFn = () => { return null; };
        protected Func<IConvertToStringComponent<TResult>> _convertToStringComponentFn = () => { return null; };
        protected Func<IDefaultValueComponent<TResult>> _defaultValueComponentFn = () => { return null; };

        public abstract TResult Prompt();
    }
}