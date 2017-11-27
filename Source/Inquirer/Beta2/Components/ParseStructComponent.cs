using System;
using InquirerCS.Beta2.Interfaces;

namespace InquirerCS.Beta2.Components
{
    public class ParseStructComponent<TResult> : IParseComponent<string, TResult> where TResult : struct
    {
        public ParseStructComponent()
        {
            Parse = value => { return value.To<TResult>(); };
        }

        public Func<string, TResult> Parse { get; }
    }
}