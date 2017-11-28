using System.Collections.Generic;

namespace InquirerCS.Beta2.Interfaces
{
    public interface IChoicesComponent<TResult>
    {
        List<TResult> Choices { get; }
    }
}