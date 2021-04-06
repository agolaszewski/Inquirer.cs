using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using InquirerCS.Components;
using InquirerCS.Interfaces;
using InquirerCS.Questions;
using InquirerCS.Traits;

namespace InquirerCS.Builders
{
    public class CheckboxBuilder<TResult>
        : IConfirmTrait<List<TResult>>,
        IConvertToStringTrait<TResult>,
        IDefaultTrait<List<TResult>>,
        IValidateResultTrait<List<TResult>>,
        IRenderQuestionTrait,
        IRenderChoicesTrait<TResult>,
        IDisplayErrorTrait,
        IWaitForInputTrait<StringOrKey>,
        IParseTrait<List<Selectable<TResult>>, List<TResult>>,
        IOnKeyTrait,
        IBuilder<Checkbox<List<TResult>, TResult>, List<TResult>>
    {
        internal CheckboxBuilder(IConsole console)
        {
            Console = console;

            this.Confirm(this, Console);
            this.ConvertToString();
            this.Default();
            this.ResultValidate();
            this.Input(Console, true, ConsoleKey.UpArrow, ConsoleKey.DownArrow, ConsoleKey.Enter, ConsoleKey.Spacebar);
            this.OnKey();
            this.RenderError(Console);
        }

        internal CheckboxBuilder(string message, IEnumerable<TResult> choices, IConsole console) : this(console)
        {
            Choices = choices.Select(item => new Selectable<TResult>(false, item)).ToList();

            this.RenderQuestion(message, this, this, Console);
            this.RenderChoices(Choices, this, Console);
            this.Parse(Choices);
        }

        [EditorBrowsable(EditorBrowsableState.Never)]
        public List<Selectable<TResult>> Choices { get; set; }

        [EditorBrowsable(EditorBrowsableState.Never)]
        public IConfirmComponent<List<TResult>> Confirm { get; set; }

        [EditorBrowsable(EditorBrowsableState.Never)]
        public IConsole Console { get; set; }

        [EditorBrowsable(EditorBrowsableState.Never)]
        public IConvertToStringComponent<TResult> Convert { get; set; }

        [EditorBrowsable(EditorBrowsableState.Never)]
        public IDefaultValueComponent<List<TResult>> Default { get; set; }

        [EditorBrowsable(EditorBrowsableState.Never)]
        public IDisplayErrorComponent DisplayError { get; set; }

        [EditorBrowsable(EditorBrowsableState.Never)]
        public IWaitForInputComponent<StringOrKey> Input { get; set; }

        [EditorBrowsable(EditorBrowsableState.Never)]
        public IOnKey OnKey { get; set; }

        [EditorBrowsable(EditorBrowsableState.Never)]
        public IParseComponent<List<Selectable<TResult>>, List<TResult>> Parse { get; set; }

        [EditorBrowsable(EditorBrowsableState.Never)]
        public IRenderChoices<TResult> RenderChoices { get; set; }

        [EditorBrowsable(EditorBrowsableState.Never)]
        public IRenderQuestionComponent RenderQuestion { get; set; }

        [EditorBrowsable(EditorBrowsableState.Never)]
        public IValidateComponent<List<TResult>> ResultValidators { get; set; }

        public Checkbox<List<TResult>, TResult> Build()
        {
            return new Checkbox<List<TResult>, TResult>(Choices, Confirm, RenderQuestion, Input, Parse, RenderChoices, ResultValidators, DisplayError, OnKey);
        }

        public PagedCheckboxBuilder<TResult> Page(int pageSize)
        {
            return new PagedCheckboxBuilder<TResult>(this, pageSize);
        }

        public List<TResult> Prompt()
        {
            return Build().Prompt();
        }

        public virtual CheckboxBuilder<TResult> WithConfirmation()
        {
            this.Confirm(this, Console);
            return this;
        }

        public virtual CheckboxBuilder<TResult> WithConvertToString(Func<TResult, string> fn)
        {
            this.ConvertToString(fn);
            return this;
        }

        public CheckboxBuilder<TResult> WithValidation(Func<List<TResult>, bool> fn, Func<List<TResult>, string> errorMessageFn)
        {
            ResultValidators.Add(fn, errorMessageFn);
            return this;
        }

        public CheckboxBuilder<TResult> WithValidation(Func<List<TResult>, bool> fn, string errorMessage)
        {
            ResultValidators.Add(fn, errorMessage);
            return this;
        }
    }
}
