using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;

namespace ConsoleWizard
{
    public class Inquirer<TAnswers> where TAnswers : class, new()
    {
        public AppAction CurrentQuestion { get; set; }

        public Inquirer()
        {
            Answers = new TAnswers();
        }

        public TAnswers Answers { get; set; }

        public InquirerFor<TAnswers, TResult> For<TResult>(Expression<Func<TAnswers, TResult>> answerProperty)
        {
            var propertyInfo = ((MemberExpression)answerProperty.Body).Member as PropertyInfo;
            if (propertyInfo == null)
            {
                throw new ArgumentException("The lambda expression 'property' should point to a valid Property");
            }

            CurrentQuestion = new AppAction()
            {
                PropertyInfo = propertyInfo
            };

            return new InquirerFor<TAnswers, TResult>(this);
        }

        
       
    }

    public class AppAction
    {
        public PropertyInfo PropertyInfo { get; set; }
        public Action<PropertyInfo> Action { get; set; }
    }
}