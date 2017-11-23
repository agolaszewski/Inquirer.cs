using System;

namespace InquirerCS.Beta2.Interfaces
{
    public interface IParseComponent<TInput, TResult>
    {
        Func<TInput, TResult> Parse { get; set; }
    }
}