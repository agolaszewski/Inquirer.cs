using System.Reflection;

namespace ConsoleWizard
{
    public class Question<T>
    {
        public string Number { get; set; }
        public FluentInquire<T> _inquire { get; set; }
        public PropertyInfo _propertyInfo { get; set; }

        public Question(string number, FluentInquire<T> inquire, PropertyInfo propertyInfo)
        {
            Number = number;
            _inquire = inquire;
            _propertyInfo = propertyInfo;
        }

        public T Prompt()
        {
            return _inquire.Prompt();
        }
    }
}