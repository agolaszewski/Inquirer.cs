using InquirerCS.Interfaces;

namespace InquirerCS.Traits
{
    public interface IPagingTrait<TResult>
    {
        IPagingComponent<TResult> Paging { get; set; }
    }
}
