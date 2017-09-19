using ConsoleWizard.Events;
using System;
using System.Reflection;

namespace ConsoleWizard
{
    public abstract class FluentInquire
    {
        public abstract IEvent Prompt();
    }

    public class FluentInquire<T> : FluentInquire
    {
        private InquireBase<T> _inquire;

        public FluentInquire(InquireBase<T> inquire)
        {
            _inquire = inquire;
        }

        public FluentInquire<T> WithDefaultValue(T defaultValue)
        {
            _inquire.DefaultValue = defaultValue;
            _inquire.HasDefaultValue = true;
            return this;
        }

        public FluentInquire<T> WithConfirmation()
        {
            _inquire.HasConfirmation = true;
            return this;
        }

        public override IEvent Prompt()
        {
            return _inquire.Prompt();
        }
    }
}