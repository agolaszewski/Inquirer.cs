using System;
using System.Collections.Generic;
using System.Linq;
using InquirerCS.Interfaces;

namespace InquirerCS.Builders
{
    public class DefaultDictionaryValueComponent<TKey, TResult> : IDefaultValueComponent<TResult> where TResult : IComparable
    {
        public DefaultDictionaryValueComponent()
        {
            HasDefaultValue = false;
            DefaultValue = default(TResult);
        }

        public DefaultDictionaryValueComponent(Dictionary<TKey, TResult> choices, TResult defaultValue)
        {
            if (choices.Any(item => item.Value.CompareTo(defaultValue) == 0))
            {
                throw new ArgumentNullException("defaultValue not found in choices collection");
            }

            HasDefaultValue = true;
            DefaultValue = defaultValue;
        }

        public TResult DefaultValue { get; }

        public bool HasDefaultValue { get; }
    }
}