using System.Collections.Generic;

namespace InquirerCS.Interfaces
{
    public interface IPagingComponent<TResult>
    {
        List<TResult> CurrentPage { get; }

        int CurrentPageNumber { get; }

        Dictionary<int, List<TResult>> PagedChoices { get; }

        bool Next();

        bool Previous();
    }
}
