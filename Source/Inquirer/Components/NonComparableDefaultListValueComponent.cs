using System;
using System.Collections.Generic;
using System.Linq;
using InquirerCS.Interfaces;

namespace InquirerCS.Components
{
    internal class NonComparableDefaultListValueComponent<TResult> : IDefaultValueComponent<TResult>
    {
        public NonComparableDefaultListValueComponent()
        {
            HasDefault = false;
            Value = default(TResult);
        }

        public NonComparableDefaultListValueComponent(List<TResult> choices, Func<TResult, bool> compareTo)
        {
            HasDefault = true;

            if (!choices.Any(item => compareTo(item)))
            {
                throw new ArgumentNullException("defaultValue not found in choices collection");
            }

            var index = choices.Select((answer, i) => new { Value = answer, Index = i }).First(item => compareTo(item.Value)).Index;
            var selected = choices.ElementAt(index);
            choices.RemoveAt(index);

            choices.Insert(0, selected);

            Value = choices[0];
        }

        public bool HasDefault { get; }

        public TResult Value { get; }
    }
}
