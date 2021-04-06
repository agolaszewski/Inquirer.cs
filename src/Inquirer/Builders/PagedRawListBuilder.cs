using System;
using System.Collections.Generic;
using System.ComponentModel;
using InquirerCS.Interfaces;
using InquirerCS.Questions;
using InquirerCS.Traits;

namespace InquirerCS.Builders
{
    public class PagedRawListBuilder<TResult> : InputBuilder<PagedRawList<TResult>, string, TResult>, IRenderChoicesTrait<TResult>, IPagingTrait<TResult>
    {
        internal PagedRawListBuilder(RawListBuilder<TResult> listBuilder, int pageSize) : base(listBuilder.Console)
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

            this.Input(Console, ConsoleKey.LeftArrow, ConsoleKey.RightArrow, ConsoleKey.Enter);
            Input.AllowTypeFn = value => { return char.IsNumber(value); };

            this.Parse(value =>
            {
                return Paging.CurrentPage[value.To<int>() - 1];
            });

            InputValidators.Add(value => { return string.IsNullOrEmpty(value) == false || Default.HasDefault; }, "Empty line");
            InputValidators.Add(value => { return value.ToN<int>().HasValue; }, value => { return $"Cannot parse {value} to {typeof(TResult)}"; });
            InputValidators.Add(
            value =>
            {
                var index = value.To<int>();
                return index > 0 && index <= Paging.CurrentPage.Count;
            },
            value =>
            {
                return $"Chosen number must be between 1 and {Paging.CurrentPage.Count} ";
            });

            OnKey = listBuilder.OnKey;
            this.Paging(listBuilder.Choices, pageSize);
        }

        [EditorBrowsable(EditorBrowsableState.Never)]
        public List<TResult> Choices { get; private set; }

        [EditorBrowsable(EditorBrowsableState.Never)]
        public IPagingComponent<TResult> Paging { get; set; }

        [EditorBrowsable(EditorBrowsableState.Never)]
        public IRenderChoices<TResult> RenderChoices { get; set; }

        public override PagedRawList<TResult> Build()
        {
            return new PagedRawList<TResult>(Paging, Confirm, RenderQuestion, Input, Parse, RenderChoices, ResultValidators, InputValidators, DisplayError, OnKey, Console);
        }
    }
}
