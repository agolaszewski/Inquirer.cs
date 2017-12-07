using System;
using InquirerCS.Components;
using InquirerCS.Interfaces;

namespace InquirerCS.Builders
{
    public abstract class Builder<TQuestion, TInput, TResult> : IBuilder<TQuestion, TResult>
    {
        protected IConfirmComponent<TResult> _confirmComponent;

        protected Func<IConfirmComponent<TResult>> _confirmComponentFn = () => { return null; };

        protected IConvertToStringComponent<TResult> _convertToStringComponent;

        protected Func<IConvertToStringComponent<TResult>> _convertToStringComponentFn = () => { return null; };

        protected IDefaultValueComponent<TResult> _defaultValueComponent;

        protected Func<IDefaultValueComponent<TResult>> _defaultValueComponentFn = () => { return null; };

        protected IDisplayQuestionComponent _displayQuestionComponent;

        protected IDisplayErrorComponent _errorDisplay;

        protected IParseComponent<TInput, TResult> _parseComponent;

        protected IValidateComponent<TInput> _validationInputComponent;

        protected IValidateComponent<TResult> _validationResultComponent;

        public IWaitForInputComponent<StringOrKey> _inputComponent { get; internal set; }

        public abstract TQuestion Build();

        public abstract TResult Prompt();
    }
}
