using InquirerCS.Components;
using InquirerCS.Interfaces;
using InquirerCS.Traits;

namespace InquirerCS
{
    public class EmptyNode<TBuilder, TQuestion, TResult> : Node<TBuilder, TQuestion, TResult> where TBuilder : IWaitForInputTrait<StringOrKey>, IOnKeyTrait, IBuilder<TQuestion, TResult> where TQuestion : IQuestion<TResult>
    {
        public EmptyNode() : base()
        {
        }

        public override bool Run()
        {
            return true;
        }
    }
}