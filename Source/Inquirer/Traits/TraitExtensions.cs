using System;
using System.Collections.Generic;
using InquirerCS.Components;

namespace InquirerCS.Traits
{
    public static class TraitExtensions
    {
        public static void Confirm<TResult>(this IConfirmTrait<List<TResult>> trait, IConvertToStringTrait<TResult> convert)
        {
            trait.Confirm = new ConfirmListComponent<List<TResult>, TResult>(convert);
        }

        public static void Confirm<TResult>(this IConfirmTrait<TResult> trait, IConvertToStringTrait<TResult> convert)
        {
            trait.Confirm = new NoConfirmationComponent<TResult>();
        }

        public static void ConvertToString<TResult>(this IConvertToStringTrait<TResult> trait)
        {
            trait.Convert = new ConvertToStringComponent<TResult>();
        }

        public static void ConvertToString<TResult>(this IConvertToStringTrait<TResult> trait, Func<TResult, string> convertFn)
        {
            trait.Convert = new ConvertToStringComponent<TResult>(convertFn);
        }

        public static void Default<TResult>(this IDefaultTrait<List<TResult>> trait, List<Selectable<TResult>> choices, List<TResult> defaultValues) where TResult : IComparable
        {
            trait.Default = new DefaultSelectedValueComponent<TResult>(choices, defaultValues);
        }

        public static void Default<TResult>(this IDefaultTrait<TResult> trait)
        {
            trait.Default = new DefaultValueComponent<TResult>();
        }

        public static void Default<TResult>(this IDefaultTrait<TResult> trait, TResult defaultValue)
        {
            trait.Default = new DefaultValueComponent<TResult>(defaultValue);
        }

        public static void RenderChoices<TResult>(this IRenderChoicesTrait<TResult> trait, List<Selectable<TResult>> choices, IConvertToStringTrait<TResult> convert)
        {
            trait.RenderChoices = new DisplaySelectableChoices<TResult>(choices, convert);
        }

        public static void RenderChoices<TResult>(this IRenderChoicesTrait<TResult> trait, List<TResult> choices, IConvertToStringTrait<TResult> convert)
        {
            trait.RenderChoices = new DisplayChoices<TResult>(choices, convert);
        }

        public static void RenderQuestion<TResult>(this IRenderQuestionTrait trait, string message, IConvertToStringTrait<TResult> convert, IDefaultTrait<List<TResult>> @default)
        {
            trait.RenderQuestion = new DisplayListQuestion<List<TResult>, TResult>(message, convert, @default);
        }

        public static void RenderQuestion<TResult>(this IRenderQuestionTrait trait, string message, IConvertToStringTrait<TResult> convert, IDefaultTrait<TResult> @default)
        {
            trait.RenderQuestion = new DisplayQuestion<TResult>(message, convert, @default);
        }

        public static void Validate<T>(this IValidateTrait<T> trait)
        {
            trait.Validators = new ValidationComponent<T>();
        }

        public static void Input(this IWaitForInputTrait<StringOrKey> trait, params ConsoleKey[] intteruptedKeys)
        {
            trait.Input = new StringOrKeyInputComponent(intteruptedKeys);
        }

        public static void OnKey(this IOnKeyTrait trait)
        {
            trait.OnKey = new OnNothing();
        }

        public static void Parse<TResult>(this IParseTrait<string, TResult> trait) where TResult : struct
        {
            trait.Parse = new ParseComponent<string, TResult>(value => value.To<TResult>());
        }

        public static void Parse<TResult>(this IParseTrait<List<Selectable<TResult>>, List<TResult>> trait, List<Selectable<TResult>> choices)
        {
            trait.Parse = new ParseSelectableListComponent<List<TResult>, TResult>(choices);
        }
    }
}