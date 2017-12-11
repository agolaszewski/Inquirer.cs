using InquirerCS.Interfaces;

namespace InquirerCS.Traits
{
    public interface IConvertToStringTrait<TResult>
    {
        IConvertToStringComponent<TResult> Convert { get; set; }
    }
}
