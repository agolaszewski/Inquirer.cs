using System.Collections.Generic;

namespace InquirerCS.Beta2.Interfaces
{
    public interface IChoicesComponent<TResult>
    {
        IList<TResult> Choices { get; }
    }
}