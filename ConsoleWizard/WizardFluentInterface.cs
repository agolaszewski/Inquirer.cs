using System;
using System.Linq;

namespace ConsoleWizard
{
    public class WizardFluentInterface<TAnswers, T> where TAnswers : new()
    {
        private Wizard<TAnswers> _wizzard;
        private Question<T> _question;

        public Action<T> NavigateFn { get; internal set; } = v => { };

        public WizardFluentInterface(Wizard<TAnswers> wizzard, Question<T> question)
        {
            _wizzard = wizzard;
            _question = question;
        }

        public WizardFluentInterface<TAnswers, T> Navigate(Func<T, string> navigateFn)
        {
            Navigate(x =>
            {
                var questionNumber = navigateFn(x);
                if (string.IsNullOrWhiteSpace(questionNumber) == false)
                {
                    _wizzard.Questions[questionNumber].Prompt();
                }
            });
            return this;
        }

        public WizardFluentInterface<TAnswers, T> Navigate(string number)
        {
            Navigate(x => { _wizzard.Questions[number].Prompt(); });
            return this;
        }

        public WizardFluentInterface<TAnswers, T> NavigateNext()
        {
            Navigate(x =>
            {
                var next = _wizzard.Questions.SkipWhile(k => k.Key != _question.Number).Skip(1).FirstOrDefault();
                _wizzard.Questions[next.Key].Prompt();
            });

            return this;
        }

        private void Navigate(Action<T> navigationFn)
        {
            NavigateFn = navigationFn;
        }


        public void Prompt()
        {
            T answer = _question.Prompt();
            NavigateFn(answer);
        }
    }
}