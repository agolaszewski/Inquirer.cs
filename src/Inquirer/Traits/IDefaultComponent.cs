using InquirerCS.Interfaces;

namespace InquirerCS.Traits
{
    public interface IDefaultTrait<TResult>
    {
        IDefaultValueComponent<TResult> Default { get; set; }
    }
}
