using System.Collections.Generic;
using System.Linq;
using InquirerCS.Beta2.Interfaces;

namespace InquirerCS.Beta2.Components
{
    public class ListChoices<TResult> : IChoicesComponent<TResult>
    {
        public ListChoices(List<TResult> choices)
        {
            Choices = choices;
        }

        public ListChoices(IEnumerable<TResult> choices)
        {
            Choices = choices.ToList();
        }

        public List<TResult> Choices { get; }
    }
}