using System;
using System.Collections.Generic;
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
        IPagingTrait<Selectable<TResult>>
    {
        public PagedCheckboxBuilder(CheckboxBuilder<TResult> checkboxBuilder, int pageSize)
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

        public List<Selectable<TResult>> Choices { get; set; }

        public IConfirmComponent<List<TResult>> Confirm { get; set; }

        public IConsole Console { get; set; }

        public IConvertToStringComponent<TResult> Convert { get; set; }

        public IDefaultValueComponent<List<TResult>> Default { get; set; }

        public IDisplayErrorComponent DisplayError { get; set; }

        public IWaitForInputComponent<StringOrKey> Input { get; set; }

        public IOnKey OnKey { get; set; }

        public IPagingComponent<Selectable<TResult>> Paging { get; set; }

        public IParseComponent<Dictionary<int, List<Selectable<TResult>>>, List<TResult>> Parse { get; set; }

        public IRenderChoices<TResult> RenderChoices { get; set; }

        public IRenderQuestionComponent RenderQuestion { get; set; }

        public IValidateComponent<List<TResult>> ResultValidators { get; set; }

        public PagedCheckbox<List<TResult>, TResult> Build()
        {
            return new PagedCheckbox<List<TResult>, TResult>(Paging, Confirm, RenderQuestion, Input, Parse, RenderChoices, ResultValidators, DisplayError, OnKey);
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
