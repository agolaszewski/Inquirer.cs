using System;
using System.Collections.Generic;
using InquirerCS.Interfaces;
using InquirerCS.Traits;

namespace InquirerCS.Builders
{
    public class PagedListBuilder<TResult> : ListBuilder<TResult>, IPagingTrait<TResult> where TResult : IComparable
    {
        public PagedListBuilder(string message, List<TResult> choices, int pageSize) : base(message, choices)
        {
            this.RenderChoices(this, this);
            this.Paging(choices, pageSize);
            this.Input(ConsoleKey.LeftArrow, ConsoleKey.RightArrow, ConsoleKey.DownArrow, ConsoleKey.UpArrow);
            this.RenderChoices(this, this);
        }

        public IPagingComponent<TResult> Paging { get; set; }
    }
}
