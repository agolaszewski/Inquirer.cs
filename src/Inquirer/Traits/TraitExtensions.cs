using System;
using System.Collections.Generic;
using System.Linq;
using InquirerCS.Components;

namespace InquirerCS.Traits
{
    public static class TraitExtensions
    {
        public static void Confirm<TResult>(this IConfirmTrait<List<TResult>> trait, IConvertToStringTrait<TResult> convert, IConsole console)
        {
            trait.Confirm = new ConfirmListComponent<List<TResult>, TResult>(convert, console);
        }

        public static void Confirm<TResult>(this IConfirmTrait<TResult> trait)
        {
            trait.Confirm = new NoConfirmationComponent<TResult>();
        }

        public static void Confirm<TResult>(this IConfirmTrait<TResult> trait, IConvertToStringTrait<TResult> convert, IConsole console)
        {
            trait.Confirm = new ConfirmComponent<TResult>(convert, console);
        }

        public static void ConvertToString<TResult>(this IConvertToStringTrait<TResult> trait)
        {
            trait.Convert = new ConvertToStringComponent<TResult>();
        }

        public static void ConvertToString<TResult>(this IConvertToStringTrait<TResult> trait, Func<TResult, string> convertFn)
        {
            trait.Convert = new ConvertToStringComponent<TResult>(convertFn);
        }

        public static void Default<TResult>(this IDefaultTrait<List<TResult>> trait, List<Selectable<TResult>> choices, Func<TResult, bool> compareTo)
        {
            trait.Default = new NonComperableDefaultSelectedValueComponent<TResult>(choices, compareTo);
        }

        public static void Default<TResult>(this IDefaultTrait<List<TResult>> trait, List<Selectable<TResult>> choices, List<TResult> defaultValues) where TResult : IComparable
        {
            trait.Default = new DefaultSelectedValueComponent<TResult>(choices, defaultValues);
        }

        public static void Default<TResult>(this IDefaultTrait<TResult> trait)
        {
            trait.Default = new DefaultValueComponent<TResult>();
        }

        public static void Default<TResult>(this IDefaultTrait<TResult> trait, List<TResult> choices, TResult defaultValues) where TResult : IComparable
        {
            trait.Default = new DefaultListValueComponent<TResult>(choices, defaultValues);
        }

        public static void Default<TResult>(this IDefaultTrait<TResult> trait, TResult defaultValue)
        {
            trait.Default = new DefaultValueComponent<TResult>(defaultValue);
        }

        public static void Input(this IWaitForInputTrait<StringOrKey> trait, IConsole console)
        {
            trait.Input = new StringOrKeyInputComponent(console);
        }

        public static void Input(this IWaitForInputTrait<StringOrKey> trait, IConsole console, bool onlyIntteruptedKeys, params ConsoleKey[] intteruptedKeys)
        {
            Func<char, bool> allowFn = null;
            if (onlyIntteruptedKeys)
            {
                List<char> chars = intteruptedKeys.Select(item => (char)item).ToList();
                allowFn = value =>
                {
                    return chars.Any(item => item == value);
                };
            }

            trait.Input = new StringOrKeyInputComponent(console, allowFn, intteruptedKeys);
        }

        public static void Input(this IWaitForInputTrait<StringOrKey> trait, IConsole console, Func<char, bool> allowFn = null, params ConsoleKey[] intteruptedKeys)
        {
            trait.Input = new StringOrKeyInputComponent(console, allowFn, intteruptedKeys);
        }

        public static void Input(this IWaitForInputTrait<StringOrKey> trait, IConsole console, params ConsoleKey[] intteruptedKeys)
        {
            trait.Input = new StringOrKeyInputComponent(console, null, intteruptedKeys);
        }

        public static void InputValidate<T>(this IValidateInputTrait<T> trait)
        {
            trait.InputValidators = new ValidationComponent<T>();
        }

        public static void OnKey(this IOnKeyTrait trait)
        {
            trait.OnKey = new OnNothing();
        }

        public static void Paging<TResult>(this IPagingTrait<TResult> trait, List<TResult> chocies, int pageSize)
        {
            trait.Paging = new PagingComponent<TResult>(chocies, pageSize);
        }

        public static void Parse<TResult>(this IParseTrait<ConsoleKey, TResult> trait, Dictionary<ConsoleKey, TResult> choices)
        {
            trait.Parse = new ParseDictionaryComponent<ConsoleKey, TResult>(choices);
        }

        public static void Parse<TResult>(this IParseTrait<Dictionary<int, List<Selectable<TResult>>>, List<TResult>> trait, IPagingTrait<Selectable<TResult>> paging)
        {
            trait.Parse = new ParseSelectablePagedListComponent<List<TResult>, TResult>(paging);
        }

        public static void Parse<TResult>(this IParseTrait<int, TResult> trait, List<TResult> choices)
        {
            trait.Parse = new ParseListComponent<TResult>(choices);
        }

        public static void Parse<TResult>(this IParseTrait<List<Selectable<TResult>>, List<TResult>> trait, List<Selectable<TResult>> choices)
        {
            trait.Parse = new ParseSelectableListComponent<List<TResult>, TResult>(choices);
        }

        public static void Parse<TResult>(this IParseTrait<string, TResult> trait) where TResult : struct
        {
            trait.Parse = new ParseComponent<string, TResult>(value => value.To<TResult>());
        }

        public static void Parse<TInput, TResult>(this IParseTrait<TInput, TResult> trait, Func<TInput, TResult> parseFn)
        {
            trait.Parse = new ParseComponent<TInput, TResult>(parseFn);
        }

        public static void PasswordInput(this IWaitForInputTrait<StringOrKey> trait, IConsole console)
        {
            trait.Input = new HideReadStringComponent(console);
        }

        public static void RenderChoices<TResult>(this IRenderChoicesTrait<TResult> trait, Dictionary<ConsoleKey, TResult> choices, IConvertToStringTrait<TResult> convert, IConsole console)
        {
            trait.RenderChoices = new DisplayExtendedChoices<TResult>(choices, convert, console);
        }

        public static void RenderChoices<TResult>(this IRenderChoicesTrait<TResult> trait, IPagingTrait<Selectable<TResult>> paging, IConvertToStringTrait<TResult> convert, IConsole console)
        {
            trait.RenderChoices = new DisplaySelectablePagedChoices<TResult>(paging, convert, console);
        }

        public static void RenderChoices<TResult>(this IRenderChoicesTrait<TResult> trait, IPagingTrait<TResult> paging, IConvertToStringTrait<TResult> convert, IConsole console)
        {
            trait.RenderChoices = new DisplayPagedListChoices<TResult>(paging, convert, console);
        }

        public static void RenderChoices<TResult>(this IRenderChoicesTrait<TResult> trait, List<Selectable<TResult>> choices, IConvertToStringTrait<TResult> convert, IConsole console)
        {
            trait.RenderChoices = new DisplaySelectableChoices<TResult>(choices, convert, console);
        }

        public static void RenderChoices<TResult>(this IRenderChoicesTrait<TResult> trait, List<TResult> choices, IConvertToStringTrait<TResult> convert, IConsole console)
        {
            trait.RenderChoices = new DisplayChoices<TResult>(choices, convert, console);
        }

        public static void RenderConfirmQuestion(this IRenderQuestionTrait trait, string message, IConvertToStringTrait<bool> convert, IDefaultTrait<bool> @default, IConsole console)
        {
            trait.RenderQuestion = new DisplayConfirmQuestion<bool>(message, convert, @default, console);
        }

        public static void RenderError(this IDisplayErrorTrait trait, IConsole console)
        {
            trait.DisplayError = new DisplayErrorCompnent(console);
        }

        public static void RenderQuestion<TResult>(this IRenderQuestionTrait trait, string message, IConvertToStringTrait<TResult> convert, IDefaultTrait<List<TResult>> @default, IConsole console)
        {
            trait.RenderQuestion = new DisplayListQuestion<List<TResult>, TResult>(message, convert, @default, console);
        }

        public static void RenderQuestion<TResult>(this IRenderQuestionTrait trait, string message, IConvertToStringTrait<TResult> convert, IDefaultTrait<TResult> @default, IConsole console)
        {
            trait.RenderQuestion = new RenderQuestion<TResult>(message, convert, @default, console);
        }

        public static void RenderRawChoices<TResult>(this IRenderChoicesTrait<TResult> trait, IPagingTrait<TResult> paging, IConvertToStringTrait<TResult> convert, IConsole console)
        {
            trait.RenderChoices = new DisplaPagedRawChoices<TResult>(paging, convert, console);
        }

        public static void RenderRawChoices<TResult>(this IRenderChoicesTrait<TResult> trait, List<TResult> choices, IConvertToStringTrait<TResult> convert, IConsole console)
        {
            trait.RenderChoices = new DisplaRawChoices<TResult>(choices, convert, console);
        }

        public static void ResultValidate<T>(this IValidateResultTrait<T> trait)
        {
            trait.ResultValidators = new ValidationComponent<T>();
        }
    }
}
