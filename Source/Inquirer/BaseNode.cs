using System;

namespace InquirerCS
{
    public abstract class BaseNode
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        public BaseNode Parent { get; set; }

        public abstract void Run();
    }
}
