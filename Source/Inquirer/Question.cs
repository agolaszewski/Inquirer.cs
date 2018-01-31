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

        public static MenuBuilder Menu(string header = null)
        {
            return new MenuBuilder(header, _console);
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
