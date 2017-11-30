using System;
using InquirerCS.Interfaces;

namespace InquirerCS.Components
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
