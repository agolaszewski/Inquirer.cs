using System;
using System.Collections.Generic;
using System.ComponentModel;
using InquirerCS.Components;
using InquirerCS.Interfaces;
using InquirerCS.Questions;
using InquirerCS.Traits;

namespace InquirerCS.Builders
{
    public class PagedCheckboxBuilder<TResult>
        : IConfirmTrait<List<TResult>>,
        IConvertToStringTrait<TResult>,
        IDefaultTrait<List<TResult>>,
        IValidateResultTrait<List<TResult>>,
        IRenderQuestionTrait,
        IRenderChoicesTrait<TResult>,
        IDisplayErrorTrait,
        IWaitForInputTrait<StringOrKey>,
        IParseTrait<Dictionary<int, List<Selectable<TResult>>>, List<TResult>>,
        IOnKeyTrait,
        IPagingTrait<Selectable<TResult>>,
        IBuilder<PagedCheckbox<List<TResult>, TResult>, List<TResult>>
    {
        internal PagedCheckboxBuilder(CheckboxBuilder<TResult> checkboxBuilder, int pageSize)
        {
            Choices = checkboxBuilder.Choices;
            Console = checkboxBuilder.Console;

            Confirm = checkboxBuilder.Confirm;
            Convert = checkboxBuilder.Convert;
            Default = checkboxBuilder.Default;
            ResultValidators = checkboxBuilder.ResultValidators;
            RenderQuestion = checkboxBuilder.RenderQuestion;

            this.RenderChoices(this, this, Console);
            DisplayError = checkboxBuilder.DisplayError;

            this.Input(Console);
            this.Parse(this);
            OnKey = checkboxBuilder.OnKey;
            this.Paging(checkboxBuilder.Choices, pageSize);
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
        public IPagingComponent<Selectable<TResult>> Paging { get; set; }

        [EditorBrowsable(EditorBrowsableState.Never)]
        public IParseComponent<Dictionary<int, List<Selectable<TResult>>>, List<TResult>> Parse { get; set; }

        [EditorBrowsable(EditorBrowsableState.Never)]
        public IRenderChoices<TResult> RenderChoices { get; set; }

        [EditorBrowsable(EditorBrowsableState.Never)]
        public IRenderQuestionComponent RenderQuestion { get; set; }

        [EditorBrowsable(EditorBrowsableState.Never)]
        public IValidateComponent<List<TResult>> ResultValidators { get; set; }

        public PagedCheckbox<List<TResult>, TResult> Build()
        {
            return new PagedCheckbox<List<TResult>, TResult>(Paging, Confirm, RenderQuestion, Input, Parse, RenderChoices, ResultValidators, DisplayError, OnKey);
        }

        public List<TResult> Prompt()
        {
            return Build().Prompt();
        }

        public virtual PagedCheckboxBuilder<TResult> WithConfirmation()
        {
            this.Confirm(this, Console);
            return this;
        }

        public virtual PagedCheckboxBuilder<TResult> WithConvertToString(Func<TResult, string> fn)
        {
            this.ConvertToString(fn);
            return this;
        }

        public PagedCheckboxBuilder<TResult> WithValidation(Func<List<TResult>, bool> fn, Func<List<TResult>, string> errorMessageFn)
        {
            ResultValidators.Add(fn, errorMessageFn);
            return this;
        }

        public PagedCheckboxBuilder<TResult> WithValidation(Func<List<TResult>, bool> fn, string errorMessage)
        {
            ResultValidators.Add(fn, errorMessage);
            return this;
        }
    }
}
