using System;
using System.Collections.Generic;
using InquirerCS.Builders;
using InquirerCS.Components;
using InquirerCS.Interfaces;
using InquirerCS.Traits;

namespace InquirerCS
{
    public static class BuildersExtensions
    {
        public static InputBuilder<TQuestion, TInput, TResult> WithConfirmation<TQuestion, TInput, TResult>(this InputBuilder<TQuestion, TInput, TResult> builder) where TQuestion : IQuestion<TResult>
        {
            builder.Confirm(builder, builder.Console);
            return builder;
        }

        public static InputBuilder<TQuestion, TInput, TResult> WithConvertToString<TQuestion, TInput, TResult>(this InputBuilder<TQuestion, TInput, TResult> builder, Func<TResult, string> fn) where TQuestion : IQuestion<TResult>
        {
            builder.ConvertToString(fn);
            return builder;
        }

        public static CheckboxBuilder<TResult> WithDefaultValue<TResult>(this CheckboxBuilder<TResult> builder, Func<TResult, bool> compareTo)
        {
            builder.Default(builder.Choices, compareTo);
            return builder;
        }

        public static CheckboxBuilder<TResult> WithDefaultValue<TResult>(this CheckboxBuilder<TResult> builder, List<TResult> defaultValues) where TResult : IComparable
        {
            builder.Default(builder.Choices, defaultValues);
            return builder;
        }

        public static CheckboxBuilder<TResult> WithDefaultValue<TResult>(this CheckboxBuilder<TResult> builder, TResult defaultValue) where TResult : IComparable
        {
            builder.Default(builder.Choices, new List<TResult>() { defaultValue });
            return builder;
        }

        public static InputBuilder<TQuestion, TInput, TResult> WithDefaultValue<TQuestion, TInput, TResult>(this InputBuilder<TQuestion, TInput, TResult> builder, TResult defaultValue) where TQuestion : IQuestion<TResult>
        {
            builder.Default = new DefaultValueComponent<TResult>(defaultValue);
            return builder;
        }

        public static ListBuilder<TResult> WithDefaultValue<TResult>(this ListBuilder<TResult> builder, Func<TResult, bool> compareTo)
        {
            builder.Default = new NonComparableDefaultListValueComponent<TResult>(builder.Choices, compareTo);
            return builder;
        }

        public static ListBuilder<TResult> WithDefaultValue<TResult>(this ListBuilder<TResult> builder, TResult defaultValue) where TResult : IComparable
        {
            builder.Default = new DefaultListValueComponent<TResult>(builder.Choices, defaultValue);
            return builder;
        }

        public static PagedCheckboxBuilder<TResult> WithDefaultValue<TResult>(this PagedCheckboxBuilder<TResult> builder, Func<TResult, bool> compareTo)
        {
            builder.Default(builder.Choices, compareTo);
            return builder;
        }

        public static PagedCheckboxBuilder<TResult> WithDefaultValue<TResult>(this PagedCheckboxBuilder<TResult> builder, List<TResult> defaultValues) where TResult : IComparable
        {
            builder.Default(builder.Choices, defaultValues);
            return builder;
        }

        public static PagedCheckboxBuilder<TResult> WithDefaultValue<TResult>(this PagedCheckboxBuilder<TResult> builder, TResult defaultValue) where TResult : IComparable
        {
            builder.Default(builder.Choices, new List<TResult>() { defaultValue });
            return builder;
        }

        public static PagedListBuilder<TResult> WithDefaultValue<TResult>(this PagedListBuilder<TResult> builder, Func<TResult, bool> compareTo)
        {
            builder.Default = new NonComparableDefaultListValueComponent<TResult>(builder.Choices, compareTo);
            return builder;
        }

        public static PagedListBuilder<TResult> WithDefaultValue<TResult>(this PagedListBuilder<TResult> builder, TResult defaultValue) where TResult : IComparable
        {
            builder.Default = new DefaultListValueComponent<TResult>(builder.Choices, defaultValue);
            return builder;
        }

        public static PagedRawListBuilder<TResult> WithDefaultValue<TResult>(this PagedRawListBuilder<TResult> builder, Func<TResult, bool> compareTo)
        {
            builder.Default = new NonComparableDefaultListValueComponent<TResult>(builder.Choices, compareTo);
            return builder;
        }

        public static PagedRawListBuilder<TResult> WithDefaultValue<TResult>(this PagedRawListBuilder<TResult> builder, TResult defaultValue) where TResult : IComparable
        {
            builder.Default = new DefaultListValueComponent<TResult>(builder.Choices, defaultValue);
            return builder;
        }

        public static RawListBuilder<TResult> WithDefaultValue<TResult>(this RawListBuilder<TResult> builder, Func<TResult, bool> compareTo)
        {
            builder.Default = new NonComparableDefaultListValueComponent<TResult>(builder.Choices, compareTo);
            return builder;
        }

        public static RawListBuilder<TResult> WithDefaultValue<TResult>(this RawListBuilder<TResult> builder, TResult defaultValue) where TResult : IComparable
        {
            builder.Default = new DefaultListValueComponent<TResult>(builder.Choices, defaultValue);
            return builder;
        }

        public static InputBuilder<TQuestion, TInput, TResult> WithValidation<TQuestion, TInput, TResult>(this InputBuilder<TQuestion, TInput, TResult> builder, Func<TResult, bool> fn, Func<TResult, string> errorMessageFn) where TQuestion : IQuestion<TResult>
        {
            builder.ResultValidators.Add(fn, errorMessageFn);
            return builder;
        }

        public static InputBuilder<TQuestion, TInput, TResult> WithValidation<TQuestion, TInput, TResult>(this InputBuilder<TQuestion, TInput, TResult> builder, Func<TResult, bool> fn, string errorMessage) where TQuestion : IQuestion<TResult>
        {
            builder.ResultValidators.Add(fn, errorMessage);
            return builder;
        }
    }
}
