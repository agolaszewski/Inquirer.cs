using System;

namespace ConsoleWizard
{
    public abstract class ConsoleWizardFluentInterface
    {
        public abstract void Prompt();
    }

    public class ConsoleWizardFluentInterface<T> : ConsoleWizardFluentInterface
    {
        private InquireBase<T> _inquire;

        public ConsoleWizardFluentInterface(InquireBase<T> inquire)
        {
            _inquire = inquire;
        }

        public ConsoleWizardFluentInterface<T> WithDefaultValue(T defaultValue)
        {
            _inquire.DefaultValue = defaultValue;
            _inquire.HasDefaultValue = true;
            return this;
        }

        public ConsoleWizardFluentInterface<T> WithConfirmation()
        {
            _inquire.HasConfirmation = true;
            return this;
        }

        public ConsoleWizardFluentInterface<T> HasAnswer(Action<T> resultFn)
        {
            _inquire.ResultFn = resultFn;
            return this;
        }

        public override void Prompt()
        {
            _inquire.Prompt();
        }
    }
}