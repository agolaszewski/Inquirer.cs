using System.Collections.Generic;

namespace InquirerCS.Beta2.Interfaces
{
    public interface IChoicesComponent<TResult>
    {
        ICollection<TResult> Choices { get; }
    }
}