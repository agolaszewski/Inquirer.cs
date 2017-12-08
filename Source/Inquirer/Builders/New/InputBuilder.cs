using System;
using System.Linq;
using InquirerCS.Components;
using InquirerCS.Interfaces;
using InquirerCS.Traits;

namespace InquirerCS.Builders.New
{
    public class InputBuilder<TInput, TResult>
        : IConfirmTrait<TResult>,
        IConvertToStringTrait<TResult>,
        IDefaultTrait<TResult>,
        IRenderQuestionTrait,
        IDisplayErrorTrait,
        IParseTrait<TInput, TResult>,
        IValidateInputTrait<TInput>,
        IValidateResultTrait<TResult>,
        IWaitForInputTrait<StringOrKey>,
        IOnKeyTrait
    {
        public InputBuilder()
        {
            this.Confirm(this);
            this.ConvertToString();
            this.Default();
            this.Validate();
            this.Input(Enum.GetValues(typeof(ConsoleKey)).Cast<ConsoleKey>().ToArray());
            this.OnKey();
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

        public InputBuilder<TInput, TResult> WithValidation(Func<TResult, bool> fn, Func<TResult, string> errorMessageFn)
        {
            ResultValidators.Add(fn, errorMessageFn);
            return this;
        }

        public InputBuilder<TInput, TResult> WithValidation(Func<TResult, bool> fn, string errorMessage)
        {
            ResultValidators.Add(fn, errorMessage);
            return this;
        }

        public InputBuilder<TInput, TResult> WithConfirmation()
        {
            this.Confirm(this);
            return this;
        }
    }
}