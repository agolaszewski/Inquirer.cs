using System;
using System.Linq;
using InquirerCS.Beta2.Interfaces;

namespace InquirerCS.Beta2.Components
{
    public class DefaultListValueComponent<TResult> : IDefaultValueComponent<TResult> where TResult : IComparable
    {
        public DefaultListValueComponent()
        {
            HasDefaultValue = false;
            DefaultValue = default(TResult);
        }

        public DefaultListValueComponent(IChoicesComponent<TResult> choicesComponent, TResult defaultValue)
        {
            HasDefaultValue = true;
            DefaultValue = defaultValue;

            if (!choicesComponent.Choices.Any(item => item.CompareTo(defaultValue) == 0))
            {
                throw new ArgumentNullException("defaultValue not found in choices collection");
            }

            var index = choicesComponent.Choices.Select((answer, i) => new { Value = answer, Index = i }).First(item => item.Value.CompareTo(defaultValue) == 0).Index;
            var selected = choicesComponent.Choices.ElementAt(index);
            choicesComponent.Choices.RemoveAt(index);

            choicesComponent.Choices.Insert(0, selected);
        }

        public TResult DefaultValue { get; }

        public bool HasDefaultValue { get; }
    }
}