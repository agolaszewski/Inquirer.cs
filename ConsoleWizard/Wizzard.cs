using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;

namespace ConsoleWizard
{
    public class Wizard<TAnswers> where TAnswers : new()
    {
        public TAnswers Answers { get; private set; }

        public SortedDictionary<string, WizardFluentInterface> Questions { get; private set; } = new SortedDictionary<string, WizardFluentInterface>();

        public Stack<WizardFluentInterface> Flow { get; private set; } = new Stack<WizardFluentInterface>();

        public Wizard()
        {
            Answers = new TAnswers();
        }

        public WizardFluentInterface<TAnswers, T> AddQuestion<T>(string number, FluentInquire<T> inquire, Expression<Func<TAnswers, object>> answerProperty)
        {
            var propertyInfo = ((MemberExpression)answerProperty.Body).Member as PropertyInfo;
            if (propertyInfo == null)
            {
                throw new ArgumentException("The lambda expression 'property' should point to a valid Property");
            }

            var question = new Question<T>(number, inquire, propertyInfo);
            var wizarrrrd = new WizardFluentInterface<TAnswers, T>(this, question);
            Questions.Add(number, wizarrrrd);

            return wizarrrrd;
        }

        public void Run(string number)
        {
            this.Questions[number].Prompt();
        }
    }
}