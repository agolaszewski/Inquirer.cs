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

        static Question()
        {
            _console = new AppConsole();
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

        public static InquirerMenu Menu(string header = null)
        {
            return new InquirerMenu(header);
        }

        public static PasswordBuilder Password(string message)
        {
            return new PasswordBuilder(message, _console);
        }

        public static RawListBuilder<TResult> RawList<TResult>(string message, IEnumerable<TResult> choices)
        {
            return new RawListBuilder<TResult>(message, choices, _console);
        }

        public static Node<CheckboxBuilder<TResult>, Checkbox<List<TResult>, TResult>, List<TResult>> Prompt<TResult>(CheckboxBuilder<TResult> builder)
        {
            return Prompt<CheckboxBuilder<TResult>, Checkbox<List<TResult>, TResult>, List<TResult>>(builder);
        }

        public static Node<ExtendedBuilder, InputKey<ConsoleKey>, ConsoleKey> Prompt(ExtendedBuilder builder)
        {
            return Prompt<ExtendedBuilder, InputKey<ConsoleKey>, ConsoleKey>(builder);
        }

        public static Node<ExtendedListBuilder<TResult>, ExtendedList<TResult>, TResult> Prompt<TResult>(ExtendedListBuilder<TResult> builder)
        {
            return Prompt<ExtendedListBuilder<TResult>, ExtendedList<TResult>, TResult>(builder);
        }

        public static Node<InputStringBuilder, Input<string>, string> Prompt(InputStringBuilder builder)
        {
            return Prompt<InputStringBuilder, Input<string>, string>(builder);
        }

        public static Node<InputStructBuilder<TResult>, Input<TResult>, TResult> Prompt<TResult>(InputStructBuilder<TResult> builder) where TResult : struct
        {
            return Prompt<InputStructBuilder<TResult>, Input<TResult>, TResult>(builder);
        }

        public static Node<ListBuilder<TResult>, ConsoleList<TResult>, TResult> Prompt<TResult>(ListBuilder<TResult> builder)
        {
            return Prompt<ListBuilder<TResult>, ConsoleList<TResult>, TResult>(builder);
        }

        public static Node<PagedCheckboxBuilder<TResult>, PagedCheckbox<List<TResult>, TResult>, List<TResult>> Prompt<TResult>(PagedCheckboxBuilder<TResult> builder)
        {
            return Prompt<PagedCheckboxBuilder<TResult>, PagedCheckbox<List<TResult>, TResult>, List<TResult>>(builder);
        }

        public static Node<PagedListBuilder<TResult>, PagedList<TResult>, TResult> Prompt<TResult>(PagedListBuilder<TResult> builder)
        {
            return Prompt<PagedListBuilder<TResult>, PagedList<TResult>, TResult>(builder);
        }

        public static Node<PagedRawListBuilder<TResult>, PagedRawList<TResult>, TResult> Prompt<TResult>(PagedRawListBuilder<TResult> builder)
        {
            return Prompt<PagedRawListBuilder<TResult>, PagedRawList<TResult>, TResult>(builder);
        }

        public static Node<PasswordBuilder, Input<string>, string> Prompt(PasswordBuilder builder)
        {
            return Prompt<PasswordBuilder, Input<string>, string>(builder);
        }

        public static Node<RawListBuilder<TResult>, RawList<TResult>, TResult> Prompt<TResult>(RawListBuilder<TResult> builder)
        {
            return Prompt<RawListBuilder<TResult>, RawList<TResult>, TResult>(builder);
        }

        public static Node<TBuilder, TQuestion, TResult> Prompt<TBuilder, TQuestion, TResult>(TBuilder builder) where TBuilder : IWaitForInputTrait<StringOrKey>, IOnKeyTrait, IBuilder<TQuestion, TResult> where TQuestion : IQuestion<TResult>
        {
            return new Node<TBuilder, TQuestion, TResult>(builder);
        }
    }
}