using System.ComponentModel;
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
        IOnKeyTrait,
        IBuilder<TQuestion, TResult>
        where TQuestion : IQuestion<TResult>
    {
        internal InputBuilder(IConsole console)
        {
            Console = console;

            this.Confirm();
            this.ConvertToString();
            this.Default();
            this.InputValidate();
            this.ResultValidate();
            this.Input(Console);
            this.OnKey();
            this.RenderError(Console);
        }

        [EditorBrowsable(EditorBrowsableState.Never)]
        public IConfirmComponent<TResult> Confirm { get; set; }

        [EditorBrowsable(EditorBrowsableState.Never)]
        public IConsole Console { get; set; }

        [EditorBrowsable(EditorBrowsableState.Never)]
        public IConvertToStringComponent<TResult> Convert { get; set; }

        [EditorBrowsable(EditorBrowsableState.Never)]
        public IDefaultValueComponent<TResult> Default { get; set; }

        [EditorBrowsable(EditorBrowsableState.Never)]
        public IDisplayErrorComponent DisplayError { get; set; }

        [EditorBrowsable(EditorBrowsableState.Never)]
        public IWaitForInputComponent<StringOrKey> Input { get; set; }

        [EditorBrowsable(EditorBrowsableState.Never)]
        public IValidateComponent<TInput> InputValidators { get; set; }

        [EditorBrowsable(EditorBrowsableState.Never)]
        public IOnKey OnKey { get; set; }

        [EditorBrowsable(EditorBrowsableState.Never)]
        public IParseComponent<TInput, TResult> Parse { get; set; }

        [EditorBrowsable(EditorBrowsableState.Never)]
        public IRenderQuestionComponent RenderQuestion { get; set; }

        [EditorBrowsable(EditorBrowsableState.Never)]
        public IValidateComponent<TResult> ResultValidators { get; set; }

        public abstract TQuestion Build();

        public virtual TResult Prompt()
        {
            return Build().Prompt();
        }
    }
}
