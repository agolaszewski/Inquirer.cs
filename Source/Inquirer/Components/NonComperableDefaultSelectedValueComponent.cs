using System;
using System.Collections.Generic;
using System.Linq;
using InquirerCS.Interfaces;

namespace InquirerCS.Components
{
    internal class NonComperableDefaultSelectedValueComponent<TResult> : IDefaultValueComponent<List<TResult>>
    {
        public NonComperableDefaultSelectedValueComponent(List<Selectable<TResult>> selectedChoices, Func<TResult, bool> compareTo)
        {
            HasDefault = true;
            Value = selectedChoices.Where(item => compareTo(item.Item)).Select(item => item.Item).ToList();
            foreach (var choice in selectedChoices)
            {
                if (compareTo(choice.Item))
                {
                    choice.IsSelected = true;
                }
            }
        }

        public bool HasDefault { get; }

        public List<TResult> Value { get; }
    }
}
