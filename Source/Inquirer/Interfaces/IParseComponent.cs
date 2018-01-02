using System;

namespace InquirerCS.Interfaces
{
    public interface IParseComponent<TInput, TResult>
    {
        Func<TInput, TResult> Parse { get; }
    }
}
