using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;

namespace ConsoleWizard
{
    public class Wizard<TAnswers> where TAnswers : new()
    {
        private TAnswers _anwers;
        public SortedDictionary<string, FluentInquire> Questions { get; private set; }

        public Wizard()
        {
            _anwers = new TAnswers();
            Questions = new SortedDictionary<string, FluentInquire>();
        }

        public WizardFluentInterface<TAnswers, T> AddQuestion<T>(string number, FluentInquire<T> question, Expression<Func<TAnswers, object>> answerProperty)
        {
            var propertyInfo = ((MemberExpression)answerProperty.Body).Member as PropertyInfo;
            if (propertyInfo == null)
            {
                throw new ArgumentException("The lambda expression 'property' should point to a valid Property");
            }
            question.Number = number;
            question.HasAnswer(x => { propertyInfo.SetValue(_anwers, x); });
            Questions.Add(number, question);
            return new WizardFluentInterface<TAnswers, T>(this, question);
        }

        public WizardFluentInterface<TAnswers, T> AddQuestion<T>(string number, InquireBase<T> question, Expression<Func<TAnswers, object>> answerProperty)
        {
            var inquire = new FluentInquire<T>(question);
            return AddQuestion<T>(number, inquire, answerProperty);
        }

        public void Run(string number)
        {
            Questions[number].Prompt();
        }

        public TAnswers Answers
        {
            get { return _anwers; }
        }
    }
}