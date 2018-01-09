using System;
using System.Collections.Generic;
using InquirerCS.Builders;
using InquirerCS.Components;
using InquirerCS.Interfaces;
using InquirerCS.Questions;
using InquirerCS.Traits;

namespace InquirerCS
{
    public static class Question
    {
        private static AppConsole _console;
        private static NavigationList<Tuple<Func<bool>, Action>> _questions = new NavigationList<Tuple<Func<bool>, Action>>();

        static Question()
        {
            _console = new AppConsole();
        }

        public static InputStructBuilder<TResult> Input<TResult>(string message) where TResult : struct
        {
            return new InputStructBuilder<TResult>(message, _console);
        }

        public static CheckboxBuilder<TResult> Checkbox<TResult>(string message, IEnumerable<TResult> choices)
        {
            return new CheckboxBuilder<TResult>(message, choices, _console);
        }

        public static ConfirmBuilder Confirm(string message)
        {
            return new ConfirmBuilder(message, _console);
        }

        public static ExtendedBuilder Extended(string message, params ConsoleKey[] @params)
        {
            return new ExtendedBuilder(message, _console, @params);
        }

        public static ExtendedListBuilder<TResult> ExtendedList<TResult>(string message, Dictionary<ConsoleKey, TResult> choices)
        {
            return new ExtendedListBuilder<TResult>(message, choices, _console);
        }

        public static InputStringBuilder Input(string message)
        {
            return new InputStringBuilder(message, _console);
        }

        public static ListBuilder<TResult> List<TResult>(string message, IEnumerable<TResult> choices)
        {
            return new ListBuilder<TResult>(message, choices, _console);
        }

        public static PasswordBuilder Password(string message)
        {
            return new PasswordBuilder(message, _console);
        }

        public static RawListBuilder<TResult> RawList<TResult>(string message, IEnumerable<TResult> choices)
        {
            return new RawListBuilder<TResult>(message, choices, _console);
        }

        public static void Ask<TResult>(ListBuilder<TResult> builder, Action<TResult> action)
        {
            Ask<ListBuilder<TResult>, ConsoleList<TResult>, TResult>(builder, action);
        }

        public static void Ask<TResult>(PagedListBuilder<TResult> builder, Action<TResult> action)
        {
            Ask<PagedListBuilder<TResult>, PagedList<TResult>, TResult>(builder, action);
        }

        public static void Ask<TResult>(RawListBuilder<TResult> builder, Action<TResult> action)
        {
            Ask<RawListBuilder<TResult>, RawList<TResult>, TResult>(builder, action);
        }

        public static void Ask<TResult>(PagedRawListBuilder<TResult> builder, Action<TResult> action)
        {
            Ask<PagedRawListBuilder<TResult>, PagedRawList<TResult>, TResult>(builder, action);
        }

        public static void Ask(PasswordBuilder builder, Action<string> action)
        {
            Ask<PasswordBuilder, Input<string>, string>(builder, action);
        }

        public static void Ask<TResult>(CheckboxBuilder<TResult> builder, Action<List<TResult>> action)
        {
            Ask<CheckboxBuilder<TResult>, Checkbox<List<TResult>, TResult>, List<TResult>>(builder, action);
        }

        public static void Ask<TResult>(PagedCheckboxBuilder<TResult> builder, Action<List<TResult>> action)
        {
            Ask<PagedCheckboxBuilder<TResult>, PagedCheckbox<List<TResult>, TResult>, List<TResult>>(builder, action);
        }

        public static void Ask(ExtendedBuilder builder, Action<ConsoleKey> action)
        {
            Ask<ExtendedBuilder, InputKey<ConsoleKey>, ConsoleKey>(builder, action);
        }

        public static void Ask<TResult>(ExtendedListBuilder<TResult> builder, Action<TResult> action)
        {
            Ask<ExtendedListBuilder<TResult>, ExtendedList<TResult>, TResult>(builder, action);
        }

        public static void Ask(InputStringBuilder builder, Action<string> action)
        {
            Ask<InputStringBuilder, Input<string>, string>(builder, action);
        }

        public static void Ask<TResult>(InputStructBuilder<TResult> builder, Action<TResult> action) where TResult : struct
        {
            Ask<InputStructBuilder<TResult>, Input<TResult>, TResult>(builder, action);
        }

        public static void Ask<TBuilder, TQuestion, TResult>(TBuilder builder, Action<TResult> action) where TBuilder : IWaitForInputTrait<StringOrKey>, IOnKeyTrait, IBuilder<TQuestion, TResult> where TQuestion : IQuestion<TResult>
        {
            builder.Input.IntteruptedKeys.Add(ConsoleKey.Escape);
            builder.OnKey = new OnEscape();

            _questions.Add(new Tuple<Func<bool>, Action>(() => { return true; }, () => { Ask<TBuilder, TQuestion, TResult>(builder, action); }));
            
            if (!_questions.Current.Item1())
            {
                return;
            }

            var answer = builder.Build().Prompt();
            if (builder.OnKey.IsInterrupted)
            {
                _questions.MovePrevious.Item2();
            }

            action(answer);

            if (_questions.MoveNext != null)
            {
                _questions.Current.Item2();
            }
        }
    }
}