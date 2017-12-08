using InquirerCS.Interfaces;

namespace InquirerCS.Traits
{
    public interface IValidateTrait<T>
    {
        IValidateComponent<T> Validators { get; set; }
    }
}