using System;

namespace InquirerCS
{
    public abstract class BaseNode
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        public Func<bool> Condition { get; private set; }

        public BaseNode Next { get; set; }

        public BaseNode Parent { get;  set; }

        public BaseNode Sibling { get; set; }

        public abstract void Run();
    }
}