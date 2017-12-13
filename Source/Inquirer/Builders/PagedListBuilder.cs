using System;
using System.Collections.Generic;
using InquirerCS.Interfaces;
using InquirerCS.Traits;

namespace InquirerCS.Builders
{
    public class PagedListBuilder<TResult> : ListBuilder<TResult>, IPagingTrait<TResult> where TResult : IComparable
    {
        public PagedListBuilder(string message, List<TResult> choices, int pageSize, IConsole console) : base(message, choices, console)
        {
            this.RenderChoices(this, this, console);
            this.Paging(choices, pageSize);
            this.Input(_console, ConsoleKey.LeftArrow, ConsoleKey.RightArrow, ConsoleKey.DownArrow, ConsoleKey.UpArrow);
            this.RenderChoices(this, this, console);
        }

        public IPagingComponent<TResult> Paging { get; set; }
    }
}
