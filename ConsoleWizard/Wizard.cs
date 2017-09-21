using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;

namespace ConsoleWizard
{
    public class Wizard<TAnswrs> where TAnswrs : new()
    {
        public TAnswrs Answers { get; set; }

        public Wizard()
        {
            Answers = new TAnswrs();
        }

        private Dictionary<Type, IQuestion> _questions = new Dictionary<Type, IQuestion>();
        private Dictionary<Type, PropertyInfo> _properties = new Dictionary<Type, PropertyInfo>();

        public void AddQuestionFor<TQuestion>(Expression<Func<TAnswrs, object>> forFn) where TQuestion : IQuestion, new()
        {
            var propertyInfo = ((MemberExpression)forFn.Body).Member as PropertyInfo;
            if (propertyInfo == null)
            {
                throw new ArgumentException("The lambda expression 'property' should point to a valid Property");
            }

            var question = new TQuestion();
            _questions.Add(typeof(TQuestion), question);
            _properties.Add(typeof(TQuestion), propertyInfo);
        }

        public void Run<T>()
        {
            _questions[typeof(T)].Run();
        }
    }
}