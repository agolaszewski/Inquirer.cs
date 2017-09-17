using System;

namespace ConsoleWizard
{
    public abstract class FluentInquire
    {
        public string Number { get; internal set; }

        public abstract void Prompt();
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

        internal FluentInquire HasAnswer(Action<T> resultFn)
        {
            _inquire.ResultFn = resultFn;
            return this;
        }

        public FluentInquire<T> Navigate(Action navigateFn)
        {
            _inquire.NavigateFn = x => { navigateFn(); };
            return this;
        }

        public FluentInquire<T> Navigate(Action<T> navigateFn)
        {
            _inquire.NavigateFn = navigateFn;
            return this;
        }

        public FluentInquire<T> Navigate(FluentInquire<T> navigateTo)
        {
            _inquire.NavigateFn = x => { navigateTo.Prompt(); };
            return this;
        }

        public override void Prompt()
        {
            _inquire.Prompt();
        }
    }
}