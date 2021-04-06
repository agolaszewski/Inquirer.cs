using System;
using System.Collections.Generic;
using System.Linq;
using InquirerCS.Interfaces;

namespace InquirerCS.Components
{
    internal class DefaultSelectedValueComponent<TResult> : IDefaultValueComponent<List<TResult>> where TResult : IComparable
    {
        public DefaultSelectedValueComponent(List<Selectable<TResult>> selectedChoices, List<TResult> defaultValues)
        {
            if (!defaultValues.All(x => selectedChoices.Any(s => s.Item.CompareTo(x) == 0)))
            {
                throw new ArgumentNullException("defaultValues not found in choices collection");
            }

            HasDefault = true;
            Value = defaultValues;
            foreach (var defaultValue in defaultValues)
            {
                selectedChoices.Where(x => x.Item.CompareTo(defaultValue) == 0).First().IsSelected = true;
            }
        }

        public bool HasDefault { get; }

        public List<TResult> Value { get; }
    }
}
