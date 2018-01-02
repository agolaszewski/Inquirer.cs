using InquirerCS.Interfaces;

namespace InquirerCS.Traits
{
    public interface IValidateInputTrait<TInput>
    {
        IValidateComponent<TInput> InputValidators { get; set; }
    }
}
