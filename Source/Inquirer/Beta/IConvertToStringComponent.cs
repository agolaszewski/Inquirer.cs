using System;

namespace InquirerCS.Beta
{
    public interface IConvertToStringComponent<TResult>
    {
        Func<TResult, string> Convert { get; }
    }
}