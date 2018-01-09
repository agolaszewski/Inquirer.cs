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
        private NavigationList<Tuple<Func<bool>, Action>> _questions = new NavigationList<Tuple<Func<bool>, Action>>();

        public QuestionForm Add<TResult>(ListBuilder<TResult> builder, Action<TResult> action)
        {
            Add<ListBuilder<TResult>, ConsoleList<TResult>, TResult>(builder, action);
            return this;
        }

        public QuestionForm Add<TResult>(PagedListBuilder<TResult> builder, Action<TResult> action)
        {
            Add<PagedListBuilder<TResult>, PagedList<TResult>, TResult>(builder, action);
            return this;
        }

        public QuestionForm Add<TResult>(RawListBuilder<TResult> builder, Action<TResult> action)
        {
            Add<RawListBuilder<TResult>, RawList<TResult>, TResult>(builder, action);
            return this;
        }

        public QuestionForm Add<TResult>(PagedRawListBuilder<TResult> builder, Action<TResult> action)
        {
            Add<PagedRawListBuilder<TResult>, PagedRawList<TResult>, TResult>(builder, action);
            return this;
        }

        public QuestionForm Add(PasswordBuilder builder, Action<string> action)
        {
            Add<PasswordBuilder, Input<string>, string>(builder, action);
            return this;
        }

        public QuestionForm Add<TResult>(CheckboxBuilder<TResult> builder, Action<List<TResult>> action)
        {
            Add<CheckboxBuilder<TResult>, Checkbox<List<TResult>, TResult>, List<TResult>>(builder, action);
            return this;
        }

        public QuestionForm Add<TResult>(PagedCheckboxBuilder<TResult> builder, Action<List<TResult>> action)
        {
            Add<PagedCheckboxBuilder<TResult>, PagedCheckbox<List<TResult>, TResult>, List<TResult>>(builder, action);
            return this;
        }

        public QuestionForm Add(ExtendedBuilder builder, Action<ConsoleKey> action)
        {
            Add<ExtendedBuilder, InputKey<ConsoleKey>, ConsoleKey>(builder, action);
            return this;
        }

        public QuestionForm Add<TResult>(ExtendedListBuilder<TResult> builder, Action<TResult> action)
        {
            Add<ExtendedListBuilder<TResult>, ExtendedList<TResult>, TResult>(builder, action);
            return this;
        }

        public QuestionForm Add(InputStringBuilder builder, Action<string> action)
        {
            Add<InputStringBuilder, Input<string>, string>(builder, action);
            return this;
        }

        public QuestionForm Add<TResult>(InputStructBuilder<TResult> builder, Action<TResult> action) where TResult : struct
        {
            Add<InputStructBuilder<TResult>, Input<TResult>, TResult>(builder, action);
            return this;
        }

        public void Fill()
        {
            if (_questions.Count == 0)
            {
                throw new ArgumentException("Form cannot be empty");
            }

            _questions[0].Item2.Invoke();
        }

        private QuestionForm Add<TBuilder, TQuestion, TResult>(TBuilder builder, Action<TResult> action) where TBuilder : IWaitForInputTrait<StringOrKey>, IOnKeyTrait, IBuilder<TQuestion, TResult> where TQuestion : IQuestion<TResult>
        {
            builder.Input.IntteruptedKeys.Add(ConsoleKey.Escape);
            builder.OnKey = new OnEscape();

            _questions.Add(new Tuple<Func<bool>, Action>(
                () => { return true; },
                () =>
                {
                    if (!_questions.Current.Item1())
                    {
                        _questions.MoveNext.Item2();
                        return;
                    }

                    var answer = builder.Build().Prompt();
                    if (builder.OnKey.IsInterrupted)
                    {
                        _questions.MovePrevious.Item2();
                    }

                    _questions.MoveNext.Item2();
                }));

            return this;
        }
    }
}