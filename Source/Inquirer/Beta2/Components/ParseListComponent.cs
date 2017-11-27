using System;
using System.Linq;
using InquirerCS.Beta2.Interfaces;

namespace InquirerCS.Beta2.Components
{
    public class ParseListComponent<TResult> : IParseComponent<int, TResult>
    {
        private IChoicesComponent<TResult> _choicesComponent;

        public ParseListComponent(IChoicesComponent<TResult> choicesComponent)
        {
            _choicesComponent = choicesComponent;
            Parse = value => { return _choicesComponent.Choices.ElementAt(value); };
        }

        public Func<int, TResult> Parse { get; }
    }
}