using System;
using InquirerCS.Interfaces;
using InquirerCS.Traits;

namespace InquirerCS.Components
{
    public class ThenComponent : IThen
    {
        private BaseNode _node;

        public ThenComponent(BaseNode node)
        {
            _node = node;
        }

        public void After(Action after)
        {
            _node.After(after);
        }
    }
}