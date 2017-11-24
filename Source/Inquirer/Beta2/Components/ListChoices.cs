using System.Collections.Generic;
using System.Linq;
using InquirerCS.Beta2.Interfaces;

namespace InquirerCS.Beta2.Components
{
    public class ListChoices<TResult> : IChoicesComponent<TResult>
    {
        public ListChoices(ICollection<TResult> choices)
        {
            Choices = choices;
        }

        public ListChoices(IEnumerable<TResult> choices)
        {
            Choices = choices.ToList();
        }

        public ICollection<TResult> Choices { get; }
    }
}