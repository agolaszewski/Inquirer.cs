using System;
using System.Collections.Generic;
using System.Linq;
using InquirerCS.Interfaces;
using InquirerCS.Traits;

namespace InquirerCS.Components
{
    internal class ParseSelectablePagedListComponent<TList, TResult> : IParseComponent<Dictionary<int, List<Selectable<TResult>>>, TList> where TList : List<TResult>
    {
        public ParseSelectablePagedListComponent(IPagingTrait<Selectable<TResult>> paging)
        {
            Parse = value =>
            {
                return (TList)paging.Paging.PagedChoices.SelectMany(x => x.Value).Where(x => x.IsSelected).Select(x => x.Item).ToList();
            };
        }

        public Func<Dictionary<int, List<Selectable<TResult>>>, TList> Parse { get; }
    }
}
