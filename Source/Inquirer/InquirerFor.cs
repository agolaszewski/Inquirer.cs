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

        internal InquirerFor()
        {
            return;
        }

        public InquirerFor<TAnswers, TResult> For(Expression<Func<TAnswers, TResult>> answerProperty)
        {
            var propertyInfo = ((MemberExpression)answerProperty.Body).Member as PropertyInfo;
            if (propertyInfo == null)
            {
                throw new ArgumentException("The lambda expression 'property' should point to a valid Property");
            }

            propertyInfo.SetValue(_inquirer.Answers, _result);
            return this;
        }

        public TResult Return()
        {
            return _result;
        }

        public void OnResult(Action<TResult> thenFn)
        {
            thenFn(_result);
        }

        public void Then(Action<TAnswers> thenFn)
        {
            thenFn(_inquirer.Answers);
        }

        public void Then(Action<TAnswers, TResult> thenFn)
        {
            thenFn(_inquirer.Answers, _result);
        }

        public InquirerFor<TAnswers, TConvert> Return<TConvert>(Func<TResult, TConvert> convertFn)
        {
            var result = convertFn(_result);
            return new InquirerFor<TAnswers, TConvert>(_inquirer, result);
        }
    }
}