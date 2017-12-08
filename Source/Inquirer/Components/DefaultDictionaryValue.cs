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
            HasDefault = false;
            Value = default(TResult);
        }

        public DefaultDictionaryValueComponent(Dictionary<TKey, TResult> choices, TResult defaultValue)
        {
            if (choices.Any(item => item.Value.CompareTo(defaultValue) == 0))
            {
                throw new ArgumentNullException("defaultValue not found in choices collection");
            }

            HasDefault = true;
            Value = defaultValue;
        }

        public TResult Value { get; }

        public bool HasDefault { get; }
    }
}
