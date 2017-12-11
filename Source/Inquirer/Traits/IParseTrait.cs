using InquirerCS.Interfaces;

namespace InquirerCS.Traits
{
    public interface IParseTrait<TInput, TResult>
    {
        IParseComponent<TInput, TResult> Parse { get; set; }
    }
}
