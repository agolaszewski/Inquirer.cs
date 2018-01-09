using System;
using System.Collections.Generic;
using InquirerCS.Builders;
using InquirerCS.Components;
using InquirerCS.Interfaces;
using InquirerCS.Questions;
using InquirerCS.Traits;

namespace InquirerCS
{
    public class QuestionForm
    {
        private NavigationList<Action> _questions = new NavigationList<Action>();

        public void Add<TResult>(ListBuilder<TResult> builder, Action<TResult> action)
        {
            Add<ListBuilder<TResult>, ConsoleList<TResult>, TResult>(builder, action);
        }

        public void Add<TResult>(PagedListBuilder<TResult> builder, Action<TResult> action)
        {
            Add<PagedListBuilder<TResult>, PagedList<TResult>, TResult>(builder, action);
        }

        public void Add<TResult>(RawListBuilder<TResult> builder, Action<TResult> action)
        {
            Add<RawListBuilder<TResult>, RawList<TResult>, TResult>(builder, action);
        }

        public void Add<TResult>(PagedRawListBuilder<TResult> builder, Action<TResult> action)
        {
            Add<PagedRawListBuilder<TResult>, PagedRawList<TResult>, TResult>(builder, action);
        }

        public void Add(PasswordBuilder builder, Action<string> action)
        {
            Add<PasswordBuilder, Input<string>, string>(builder, action);
        }

        public void Add<TResult>(CheckboxBuilder<TResult> builder, Action<List<TResult>> action)
        {
            Add<CheckboxBuilder<TResult>, Checkbox<List<TResult>, TResult>, List<TResult>>(builder, action);
        }

        public void Add<TResult>(PagedCheckboxBuilder<TResult> builder, Action<List<TResult>> action)
        {
            Add<PagedCheckboxBuilder<TResult>, PagedCheckbox<List<TResult>, TResult>, List<TResult>>(builder, action);
        }

        public void Add(ExtendedBuilder builder, Action<ConsoleKey> action)
        {
            Add<ExtendedBuilder, InputKey<ConsoleKey>, ConsoleKey>(builder, action);
        }

        public void Add<TResult>(ExtendedListBuilder<TResult> builder, Action<TResult> action)
        {
            Add<ExtendedListBuilder<TResult>, ExtendedList<TResult>, TResult>(builder, action);
        }

        public void Add(InputStringBuilder builder, Action<string> action)
        {
            Add<InputStringBuilder, Input<string>, string>(builder, action);
        }

        public void Add<TResult>(InputStructBuilder<TResult> builder, Action<TResult> action) where TResult : struct
        {
            Add<InputStructBuilder<TResult>, Input<TResult>, TResult>(builder, action);
        }

        public void Fill()
        {
            if (_questions.Count == 0)
            {
                throw new ArgumentException("Form cannot be empty");
            }

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