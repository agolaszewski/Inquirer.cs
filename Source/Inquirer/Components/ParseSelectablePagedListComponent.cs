using System;
using System.Collections.Generic;
using System.Linq;
using InquirerCS.Interfaces;

namespace InquirerCS.Components
{
    public class ParseSelectablePagedListComponent<TList, TResult> : IParseComponent<Dictionary<int, List<Selectable<TResult>>>, TList> where TList : List<TResult>
    {
        private Dictionary<int, List<Selectable<TResult>>> _choices;

        public ParseSelectablePagedListComponent(Dictionary<int, List<Selectable<TResult>>> choices)
        {
            _choices = choices;
            Parse = value =>
            {
                return (TList)_choices.SelectMany(x => x.Value).Where(x => x.IsSelected).Select(x => x.Item).ToList();
            };
        }

        public Func<Dictionary<int, List<Selectable<TResult>>>, TList> Parse { get; }
    }
}
