using System;
using InquirerCS.Components;
using InquirerCS.Interfaces;
using InquirerCS.Traits;

namespace InquirerCS.Builders
{
    public abstract class InputBuilder<TQuestion, TInput, TResult>
        : IConfirmTrait<TResult>,
        IConvertToStringTrait<TResult>,
        IDefaultTrait<TResult>,
        IRenderQuestionTrait,
        IDisplayErrorTrait,
        IParseTrait<TInput, TResult>,
        IValidateInputTrait<TInput>,
        IValidateResultTrait<TResult>,
        IWaitForInputTrait<StringOrKey>,
        IOnKeyTrait where TQuestion : IQuestion<TResult>
    {
        public InputBuilder()
        {
            this.Confirm(this);
            this.ConvertToString();
            this.Default();
            this.InputValidate();
            this.ResultValidate();
            this.Input();
            this.OnKey();
            this.RenderError();
        }

        public IConfirmComponent<TResult> Confirm { get; set; }

        public IConvertToStringComponent<TResult> Convert { get; set; }

        public IDefaultValueComponent<TResult> Default { get; set; }

        public IDisplayErrorComponent DisplayError { get; set; }

        public IWaitForInputComponent<StringOrKey> Input { get; set; }

        public IValidateComponent<TInput> InputValidators { get; set; }

        public IOnKey OnKey { get; set; }

        public IParseComponent<TInput, TResult> Parse { get; set; }

        public IRenderQuestionComponent RenderQuestion { get; set; }

        public IValidateComponent<TResult> ResultValidators { get; set; }

        public abstract TQuestion Build();

        public virtual InputBuilder<TQuestion, TInput, TResult> WithConfirmation()
        {
            this.Confirm(this);
            return this;
        }

        public virtual InputBuilder<TQuestion, TInput, TResult> WithDefaultValue(TResult defaultValue)
        {
            Default = new DefaultValueComponent<TResult>(defaultValue);
            return this;
        }

        public virtual InputBuilder<TQuestion, TInput, TResult> WithValidation(Func<TResult, bool> fn, Func<TResult, string> errorMessageFn)
        {
            ResultValidators.Add(fn, errorMessageFn);
            return this;
        }

        public virtual InputBuilder<TQuestion, TInput, TResult> WithValidation(Func<TResult, bool> fn, string errorMessage)
        {
            ResultValidators.Add(fn, errorMessage);
            return this;
        }
    }
}
