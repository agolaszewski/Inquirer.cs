using System;
using System.Collections.Generic;
using System.Linq;
using InquirerCS.Beta2.Interfaces;

namespace InquirerCS.Beta2.Components
{
    public class ParseListComponent<TResult> : IParseComponent<int, TResult>
    {
        private List<TResult> _choices;

        public ParseListComponent(List<TResult> choices)
        {
            _choices = choices;
            Parse = value => { return _choices.ElementAt(value); };
        }

        public Func<int, TResult> Parse { get; }
    }
}