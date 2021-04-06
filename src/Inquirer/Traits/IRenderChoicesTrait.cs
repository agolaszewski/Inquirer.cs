using InquirerCS.Interfaces;

namespace InquirerCS.Traits
{
    public interface IRenderChoicesTrait<TResult>
    {
        IRenderChoices<TResult> RenderChoices { get; set; }
    }
}
