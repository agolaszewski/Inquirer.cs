namespace ConsoleWizard
{
    
    public class FluentInquire<T>
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

        public T Prompt()
        {
            return _inquire.Prompt();
        }
    }
}