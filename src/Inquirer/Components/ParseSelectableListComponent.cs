using System;
using System.Collections.Generic;
using System.Linq;
using InquirerCS.Interfaces;

namespace InquirerCS.Components
{
    internal class ParseSelectableListComponent<TList, TResult> : IParseComponent<List<Selectable<TResult>>, TList> where TList : List<TResult>
    {
        private List<Selectable<TResult>> _choices;

        public ParseSelectableListComponent(List<Selectable<TResult>> choices)
        {
            _choices = choices;
            Parse = value =>
            {
                return (TList)_choices.Where(item => item.IsSelected).Select(item => item.Item).ToList();
            };
        }

        public Func<List<Selectable<TResult>>, TList> Parse { get; }
    }
}
