using System;
using System.Collections.Generic;
using InquirerCS.Interfaces;
using InquirerCS.Questions;
using InquirerCS.Traits;

namespace InquirerCS.Builders
{
    public class PagedRawListBuilder<TResult> : InputBuilder<PagedRawList<TResult>, string, TResult>, IRenderChoicesTrait<TResult>, IPagingTrait<TResult> where TResult : IComparable
    {
        public PagedRawListBuilder(RawListBuilder<TResult> listBuilder, int pageSize) : base(listBuilder.Console)
        {
            Choices = listBuilder.Choices;
            Console = listBuilder.Console;

            Confirm = listBuilder.Confirm;
            Convert = listBuilder.Convert;
            Default = listBuilder.Default;
            ResultValidators = listBuilder.ResultValidators;
            RenderQuestion = listBuilder.RenderQuestion;

            this.RenderRawChoices(this, this, Console);
            DisplayError = listBuilder.DisplayError;

            this.Input(Console);
            this.Parse(value =>
            {
                return Paging.CurrentPage[value.To<int>()];
            });

            OnKey = listBuilder.OnKey;
            this.Paging(listBuilder.Choices, pageSize);
        }

        public List<TResult> Choices { get; private set; }

        public IPagingComponent<TResult> Paging { get; set; }

        public IRenderChoices<TResult> RenderChoices { get; set; }

        public override PagedRawList<TResult> Build()
        {
            return new PagedRawList<TResult>(Paging, Confirm, RenderQuestion, Input, Parse, RenderChoices, ResultValidators, InputValidators, DisplayError, OnKey, Console);
        }
    }
}