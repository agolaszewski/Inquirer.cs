using System.Collections.Generic;
using System.Linq;
using InquirerCS.Beta2.Interfaces;

namespace InquirerCS.Beta2.Components
{
    public class SelectableListChoices<TResult> : IChoicesComponent<Selectable<TResult>>
    {
        public SelectableListChoices(IList<TResult> choices)
        {
            Choices = choices.Select(item => new Selectable<TResult>(false, item)).ToList();
        }

        public SelectableListChoices(IEnumerable<TResult> choices)
        {
            Choices = choices.Select(item => new Selectable<TResult>(false, item)).ToList();
        }

        public List<Selectable<TResult>> Choices { get; }
    }
}