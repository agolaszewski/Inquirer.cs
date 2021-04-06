using InquirerCS.Interfaces;

namespace InquirerCS.Traits
{
    public interface IValidateResultTrait<T>
    {
        IValidateComponent<T> ResultValidators { get; set; }
    }
}
