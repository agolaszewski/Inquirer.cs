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
        public InputBuilder(IConsole console)
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

        public IConfirmComponent<TResult> Confirm { get; set; }

        public IConsole Console { get; set; }

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

        public TResult Prompt()
        {
            return Build().Prompt();
        }
    }
}
