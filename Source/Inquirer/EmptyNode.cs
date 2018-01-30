using InquirerCS.Components;
using InquirerCS.Interfaces;
using InquirerCS.Traits;

namespace InquirerCS
{
    public class EmptyNode : BaseNode
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