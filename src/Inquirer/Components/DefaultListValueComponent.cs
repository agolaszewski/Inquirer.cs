using System;
using System.Collections.Generic;
using System.Linq;
using InquirerCS.Interfaces;

namespace InquirerCS.Components
{
    internal class DefaultListValueComponent<TResult> : IDefaultValueComponent<TResult> where TResult : IComparable
    {
        public DefaultListValueComponent()
        {
            HasDefault = false;
            Value = default(TResult);
        }

        public DefaultListValueComponent(List<TResult> choices, TResult defaultValue)
        {
            HasDefault = true;
            Value = defaultValue;

            if (!choices.Any(item => item.CompareTo(defaultValue) == 0))
            {
                throw new ArgumentNullException("defaultValue not found in choices collection");
            }

            var index = choices.Select((answer, i) => new { Value = answer, Index = i }).First(item => item.Value.CompareTo(defaultValue) == 0).Index;
            var selected = choices.ElementAt(index);
            choices.RemoveAt(index);

            choices.Insert(0, selected);
        }

        public bool HasDefault { get; }

        public TResult Value { get; }
    }
}
