using InquirerCS.Interfaces;

namespace InquirerCS.Traits
{
    public interface IConfirmTrait<TResult>
    {
        IConfirmComponent<TResult> Confirm { get; set; }
    }
}
