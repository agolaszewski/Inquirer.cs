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

        private static NavigationList<Tuple<Func<bool>>> _questions = new NavigationList<Tuple<Func<bool>>>();

        static Question()
        {
            _console = new AppConsole();
        }

        public static Node Ask()
        {
            var node = new Node(null, Node.CurrentNode);
            return node;
        }

        public static void Ask<TResult>(CheckboxBuilder<TResult> builder)
        {
            Ask<CheckboxBuilder<TResult>, Checkbox<List<TResult>, TResult>, List<TResult>>(builder);
        }

        public static void Ask(ExtendedBuilder builder)
        {
            Ask<ExtendedBuilder, InputKey<ConsoleKey>, ConsoleKey>(builder);
        }

        public static void Ask<TResult>(ExtendedListBuilder<TResult> builder)
        {
            Ask<ExtendedListBuilder<TResult>, ExtendedList<TResult>, TResult>(builder);
        }

        public static void Ask(InputStringBuilder builder)
        {
            Ask<InputStringBuilder, Input<string>, string>(builder);
        }

        public static void Ask<TResult>(InputStructBuilder<TResult> builder) where TResult : struct
        {
            Ask<InputStructBuilder<TResult>, Input<TResult>, TResult>(builder);
        }

        public static void Ask<TResult>(ListBuilder<TResult> builder)
        {
            Ask<ListBuilder<TResult>, ConsoleList<TResult>, TResult>(builder);
        }

        public static void Ask<TResult>(PagedCheckboxBuilder<TResult> builder)
        {
            Ask<PagedCheckboxBuilder<TResult>, PagedCheckbox<List<TResult>, TResult>, List<TResult>>(builder);
        }

        public static void Ask<TResult>(PagedListBuilder<TResult> builder)
        {
            Ask<PagedListBuilder<TResult>, PagedList<TResult>, TResult>(builder);
        }

        public static void Ask<TResult>(PagedRawListBuilder<TResult> builder)
        {
            Ask<PagedRawListBuilder<TResult>, PagedRawList<TResult>, TResult>(builder);
        }

        public static void Ask(PasswordBuilder builder)
        {
            Ask<PasswordBuilder, Input<string>, string>(builder);
        }

        public static void Ask<TResult>(RawListBuilder<TResult> builder)
        {
            Ask<RawListBuilder<TResult>, RawList<TResult>, TResult>(builder);
        }

        public static void Ask<TBuilder, TQuestion, TResult>(TBuilder builder) where TBuilder : IWaitForInputTrait<StringOrKey>, IOnKeyTrait, IBuilder<TQuestion, TResult> where TQuestion : IQuestion<TResult>
        {
            builder.Input.IntteruptedKeys.Add(ConsoleKey.Escape);
            builder.OnKey = new OnEscape();

            var answer = builder.Build().Prompt();
            if (builder.OnKey.IsInterrupted)
            {
                if (Node.CurrentNode.Parent != null)
                {
                    Node.CurrentNode.Next = null;
                    Node.CurrentNode.Parent.Go();
                }

                if (Node.CurrentNode.Sibling != null)
                {
                    Node.CurrentNode.Next = null;
                    Node.CurrentNode.Sibling.Go();
                }
            }
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

        public static InputStructBuilder<TResult> Input<TResult>(string message) where TResult : struct
        {
            return new InputStructBuilder<TResult>(message, _console);
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
    }
}