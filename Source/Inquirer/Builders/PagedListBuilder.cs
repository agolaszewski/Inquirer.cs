using System;
using System.Collections.Generic;
using InquirerCS.Interfaces;
using InquirerCS.Questions;
using InquirerCS.Traits;

namespace InquirerCS.Builders
{
    public class PagedListBuilder<TResult> : InputBuilder<PagedList<TResult>, int, TResult>, IRenderChoicesTrait<TResult>, IPagingTrait<TResult>
    {
        public PagedListBuilder(ListBuilder<TResult> listBuilder, int pageSize) : base(listBuilder.Console)
        {
            Choices = listBuilder.Choices;
            Console = listBuilder.Console;

            Confirm = listBuilder.Confirm;
            Convert = listBuilder.Convert;
            Default = listBuilder.Default;
            ResultValidators = listBuilder.ResultValidators;
            RenderQuestion = listBuilder.RenderQuestion;

            this.RenderChoices(this, this, Console);
            DisplayError = listBuilder.DisplayError;

            this.Input(Console);
            this.Parse(value =>
            {
                return Paging.CurrentPage[value];
            });

            OnKey = listBuilder.OnKey;
            this.Paging(listBuilder.Choices, pageSize);
        }

        public List<TResult> Choices { get; private set; }

        public IPagingComponent<TResult> Paging { get; set; }

        public IRenderChoices<TResult> RenderChoices { get; set; }

        public override PagedList<TResult> Build()
        {
            return new PagedList<TResult>(Paging, Confirm, RenderQuestion, Input, Parse, RenderChoices, ResultValidators, DisplayError, OnKey);
        }

        public PagedListBuilder<TResult> WithConfirmation()
        {
            this.Confirm(this, Console);
            return this;
        }

        public PagedListBuilder<TResult> WithConvertToString(Func<TResult, string> fn)
        {
            this.ConvertToString(fn);
            return this;
        }

        public PagedListBuilder<TResult> WithValidation(Func<TResult, bool> fn, Func<TResult, string> errorMessageFn)
        {
            ResultValidators.Add(fn, errorMessageFn);
            return this;
        }

        public PagedListBuilder<TResult> WithValidation(Func<TResult, bool> fn, string errorMessage)
        {
            ResultValidators.Add(fn, errorMessage);
            return this;
        }
    }
}
