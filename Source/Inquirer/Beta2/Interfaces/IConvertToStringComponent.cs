using System;

namespace InquirerCS.Beta2.Interfaces
{
    public interface IConvertToStringComponent<TResult>
    {
        Func<TResult, string> Convert { get; }
    }
}