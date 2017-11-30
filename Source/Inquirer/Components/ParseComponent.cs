using System;
using InquirerCS.Interfaces;

namespace InquirerCS.Components
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
