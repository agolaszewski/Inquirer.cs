using System;
using InquirerCS.Builders;
using InquirerCS.Components;
using InquirerCS.Interfaces;
using InquirerCS.Questions;
using InquirerCS.Traits;

namespace InquirerCS
{
    public class QuestionForms
    {
        private NavigationList<Action> _questions = new NavigationList<Action>();

        public void Add<TResult>(ListBuilder<TResult> builder, Action<TResult> action)
        {
            Add<ListBuilder<TResult>, ConsoleList<TResult>, TResult>(builder, action);
        }

        public void Fill()
        {
            _questions[0].Invoke();
        }

        private void Add<TBuilder, TQuestion, TResult>(TBuilder builder, Action<TResult> action) where TBuilder : IWaitForInputTrait<StringOrKey>, IOnKeyTrait, IBuilder<TQuestion, TResult> where TQuestion : IQuestion<TResult>
        {
            builder.Input.IntteruptedKeys.Add(ConsoleKey.Escape);
            builder.OnKey = new OnEscape();

            _questions.Add(() =>
            {
                var answer = builder.Build().Prompt();
                if (builder.OnKey.IsInterrupted)
                {
                    _questions.MovePrevious();
                }

                _questions.MoveNext();
            });
        }
    }
}