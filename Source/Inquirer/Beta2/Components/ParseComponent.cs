using System;
using InquirerCS.Beta2.Interfaces;

namespace InquirerCS.Beta2.Components
{
    public class ParseComponent<TInput, TResult> : IParseComponent<TInput, TResult>
    {
        public ParseComponent(Func<TInput, TResult> parseFn)
        {
            Parse = parseFn;
        }

        public Func<TInput, TResult> Parse { get; set; }
    }
}