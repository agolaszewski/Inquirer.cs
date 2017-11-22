using System;

namespace InquirerCS.Beta
{
    public interface IParseComponent<TInput, TResult>
    {
        Func<TInput, TResult> Parse { get; set; }
    }
}