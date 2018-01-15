using System;

namespace InquirerCS
{
    public abstract class BaseNode
    {
        public Func<bool> Condition { get; private set; }

        public BaseNode Next { get; set; }

        public BaseNode Parent { get; private set; }

        public BaseNode Sibling { get; set; }

        public abstract void Run();
    }
}