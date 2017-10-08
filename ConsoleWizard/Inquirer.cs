using System;
using System.Linq.Expressions;
using System.Reflection;

namespace ConsoleWizard
{
    public class Inquirer<TAnswers> where TAnswers : class, new()
    {
        public Inquirer()
        {
            Answers = new TAnswers();
        }

        public TAnswers Answers { get; set; }

        public PropertyInfo PropertyInfo { get; set; }

        public InquirerFor<TAnswers, TResult> For<TResult>(Expression<Func<TAnswers, TResult>> answerProperty)
        {
            var propertyInfo = ((MemberExpression)answerProperty.Body).Member as PropertyInfo;
            if (propertyInfo == null)
            {
                throw new ArgumentException("The lambda expression 'property' should point to a valid Property");
            }

            PropertyInfo = propertyInfo;

            return new InquirerFor<TAnswers, TResult>(this);
        }
    }
}
