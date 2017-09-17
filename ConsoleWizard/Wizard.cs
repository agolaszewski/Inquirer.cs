using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;

namespace ConsoleWizard
{
    public class Wizard<TAnswers> where TAnswers : new()
    {
        private TAnswers _anwers;
        private Dictionary<string, ConsoleWizardFluentInterface> _questions;

        public Wizard()
        {
            _anwers = new TAnswers();
            _questions = new Dictionary<string, ConsoleWizardFluentInterface>();
        }

        public void AddQuestion<T>(string number, ConsoleWizardFluentInterface<T> question, Expression<Func<TAnswers, object>> answerProperty)
        {
            var propertyInfo = ((MemberExpression)answerProperty.Body).Member as PropertyInfo;
            if (propertyInfo == null)
            {
                throw new ArgumentException("The lambda expression 'property' should point to a valid Property");
            }

            question.HasAnswer(x => { propertyInfo.SetValue(_anwers, x); });
            _questions.Add(number, question);
        }

        public void Run(string number)
        {
            _questions[number].Prompt();
        }

        public TAnswers Answers
        {
            get { return _anwers; }
        }
    }
}