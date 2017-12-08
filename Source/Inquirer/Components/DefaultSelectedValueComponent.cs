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
            HasDefault = true;
            Value = defaultValues;
            foreach (var defaultValue in defaultValues)
            {
                selectedChoices.Where(x => x.Item.CompareTo(defaultValue) == 0).First().IsSelected = true;
            }
        }

        public List<TResult> Value { get; }

        public bool HasDefault { get; }
    }
}
