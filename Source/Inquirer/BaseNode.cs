using System;

namespace InquirerCS
{
    public abstract class BaseNode
    {
        public BaseNode Child { get; set; }

        public Guid Id { get; set; } = Guid.NewGuid();

        public bool IsCurrent { get; internal set; }

        public BaseNode Next { get; set; }

        public BaseNode Parent { get; set; }

        public BaseNode Previous { get; set; }

        public int ScopeLevel { get; set; }

        public abstract void Run();
    }
}