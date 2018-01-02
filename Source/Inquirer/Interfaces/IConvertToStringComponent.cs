using System;

namespace InquirerCS.Interfaces
{
    public interface IConvertToStringComponent<TResult>
    {
        Func<TResult, string> Run { get; }
    }
}
