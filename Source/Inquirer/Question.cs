using System;
using System.Collections.Generic;
using InquirerCS.Builders;

namespace InquirerCS.Beta2
{
    public static class Question
    {
        public static CheckboxBuilder<TResult> Checkbox<TResult>(string message, IEnumerable<TResult> choices) where TResult : IComparable
        {
            return new CheckboxBuilder<TResult>(message, choices);
        }

        public static ConfirmBuilder Confirm(string message)
        {
            return new ConfirmBuilder(message);
        }

        public static ExtendedBuilder Extended(string message, params ConsoleKey[] @params)
        {
            return new ExtendedBuilder(message, @params);
        }

        public static ExtendedListBuilder<TResult> ExtendedList<TResult>(string message, IDictionary<ConsoleKey, TResult> choices) where TResult : IComparable
        {
            return new ExtendedListBuilder<TResult>(message, choices);
        }

        public static InputStringBuilder Input(string message)
        {
            return new InputStringBuilder(message);
        }

        public static InputStructBuilder<TResult> Input<TResult>(string message) where TResult : struct
        {
            return new InputStructBuilder<TResult>(message);
        }

        public static ListBuilder<TResult> List<TResult>(string message, IEnumerable<TResult> choices) where TResult : IComparable
        {
            return new ListBuilder<TResult>(message, choices);
        }

        public static PasswordBuilder Password(string message)
        {
            return new PasswordBuilder(message);
        }

        public static RawListBuilder<TResult> RawList<TResult>(string message, IEnumerable<TResult> choices) where TResult : IComparable
        {
            return new RawListBuilder<TResult>(message, choices);
        }
    }
}
