using System;
using System.Linq;

namespace ConsoleWizard
{
    public class WizardFluentInterface<TAnswers, T> where TAnswers : new()
    {
        private Wizard<TAnswers> _wizzard;
        private FluentInquire<T> _question;

        public WizardFluentInterface(Wizard<TAnswers> wizzard, FluentInquire<T> question)
        {
            _wizzard = wizzard;
            _question = question;
        }

        public WizardFluentInterface<TAnswers, T> Navigate(Func<T, string> navigateFn)
        {
            //var questionNumber = navigateFn(_question);
            //if (string.IsNullOrWhiteSpace(questionNumber) == false)
            //{
            //    _wizzard.Flow.Push(_wizzard.Questions[questionNumber]);
            //    _wizzard.Questions[questionNumber].Prompt();
            //}
            return this;
        }

        public WizardFluentInterface<TAnswers, T> Navigate(string number)
        {
            //_question.Navigate(x =>
            //{
            //    _wizzard.Flow.Push(_wizzard.Questions[number]);
            //    _wizzard.Questions[number].Prompt();
            //});
            return this;
        }

        public WizardFluentInterface<TAnswers, T> NavigateNext()
        {
            //_question.Navigate(x =>
            //{
            //    var next = _wizzard.Questions.SkipWhile(k => k.Key != _question.Number).Skip(1).FirstOrDefault();
            //    _wizzard.Flow.Push(_wizzard.Questions[next.Key]);
            //    _wizzard.Questions[next.Key].Prompt();
            //});

            return this;
        }
    }
}