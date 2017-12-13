using System;
using System.Collections.Generic;
using InquirerCS.Builders;

namespace InquirerCS
{
    public static class Question
    {
        private static AppConsole _console;

        static Question()
        {
            _console = new AppConsole();
        }

        public static InputStringBuilder _inputComponent(string message)
        {
            return new InputStringBuilder(message, _console);
        }

        public static InputStructBuilder<TResult> _inputComponent<TResult>(string message) where TResult : struct
        {
            return new InputStructBuilder<TResult>(message, _console);
        }

        public static CheckboxBuilder<TResult> Checkbox<TResult>(string message, IEnumerable<TResult> choices) where TResult : IComparable
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

        ////public static ExtendedListBuilder<TResult> ExtendedList<TResult>(string message, IDictionary<ConsoleKey, TResult> choices) where TResult : IComparable
        ////{
        ////    return new ExtendedListBuilder<TResult>(message, choices);
        ////}

        public static ListBuilder<TResult> List<TResult>(string message, IEnumerable<TResult> choices) where TResult : IComparable
        {
            return new ListBuilder<TResult>(message, choices, _console);
        }

        public static PasswordBuilder Password(string message)
        {
            return new PasswordBuilder(message, _console);
        }

        public static RawListBuilder<TResult> RawList<TResult>(string message, IEnumerable<TResult> choices) where TResult : IComparable
        {
            return new RawListBuilder<TResult>(message, choices, _console);
        }
    }
}