using System;
using System.Collections.Generic;
using InquirerCS.Interfaces;
using InquirerCS.Questions;
using InquirerCS.Traits;

namespace InquirerCS.Builders
{
    public class PagedRawListBuilder<TResult> : RawListBuilder<TResult>, IPagingTrait<TResult> where TResult : IComparable
    {
        public PagedRawListBuilder(string message, List<TResult> choices, int pageSize, IConsole console) : base(message, choices, console)
        {
            this.Parse(value =>
            {
                return Paging.CurrentPage[value.To<int>() - 1];
            });
            this.RenderChoices(this, this);
            this.Paging(choices, pageSize);
            this.Input(ConsoleKey.LeftArrow, ConsoleKey.RightArrow);

            InputValidators.Add(value => { return string.IsNullOrEmpty(value) == false || Default.HasDefault; }, "Empty line");
            InputValidators.Add(value => { return value.ToN<int>().HasValue; }, value => { return $"Cannot parse {value} to {typeof(TResult)}"; });
            InputValidators.Add(
            value =>
            {
                var index = value.To<int>();
                return index > 0 && index <= choices.Count;
            },
            value =>
            {
                return $"Choosen number must be between 1 and {Paging.CurrentPage.Count}";
            });
        }

        public IPagingComponent<TResult> Paging { get; set; }

        public new PagedRawList<TResult> Build()
        {
            return new PagedRawList<TResult>(Paging, Confirm, RenderQuestion, Input, Parse, RenderChoices, ResultValidators, InputValidators, DisplayError, OnKey);
        }
    }
}
