using System;
using System.Linq.Expressions;
using System.Reflection;

namespace InquirerCS
{
    public class InquirerFor<TAnswers, TResult> where TAnswers : class, new()
    {
        private Inquirer<TAnswers> _inquirer;

        private TResult _result;

        internal InquirerFor(Inquirer<TAnswers> inquirer, TResult result)
        {
            _inquirer = inquirer;
            _result = result;
        }

        private InquirerFor()
        {
        }

        public TResult For(Expression<Func<TAnswers, TResult>> answerProperty)
        {
            var propertyInfo = ((MemberExpression)answerProperty.Body).Member as PropertyInfo;
            if (propertyInfo == null)
            {
                throw new ArgumentException("The lambda expression 'property' should point to a valid Property");
            }

            propertyInfo.SetValue(_inquirer.Answers, _result);
            return _result;
        }

        public TResult Return()
        {
            return _result;
        }

        public InquirerFor<TAnswers, TConvert> Return<TConvert>(Func<TResult, TConvert> convertFn)
        {
            var result = convertFn(_result);
            return new InquirerFor<TAnswers, TConvert>(_inquirer, result);
        }
    }
}
