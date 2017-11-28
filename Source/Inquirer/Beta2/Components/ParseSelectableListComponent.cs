using System;
using System.Collections.Generic;
using System.Linq;
using InquirerCS.Beta2.Interfaces;

namespace InquirerCS.Beta2.Components
{
    public class ParseSelectableListComponent<TList, TResult> : IParseComponent<List<Selectable<TResult>>, TList> where TList : List<TResult>
    {
        private IChoicesComponent<Selectable<TResult>> _choicesComponent;

        public ParseSelectableListComponent(IChoicesComponent<Selectable<TResult>> choicesComponent)
        {
            _choicesComponent = choicesComponent;
            Parse = value =>
            {
                return (TList)_choicesComponent.Choices.Where(item => item.IsSelected).Select(item => item.Item).ToList();
            };
        }

        public Func<List<Selectable<TResult>>, TList> Parse { get; }
    }
}