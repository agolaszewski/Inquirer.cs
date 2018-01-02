using InquirerCS.Interfaces;

namespace InquirerCS.Traits
{
    public interface IWaitForInputTrait<TResult>
    {
        IWaitForInputComponent<TResult> Input { get; set; }
    }
}
