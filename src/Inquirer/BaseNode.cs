using System;

namespace InquirerCS
{
    public abstract class BaseNode
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        public abstract bool Run();

        internal abstract void After(Action after);
    }
}