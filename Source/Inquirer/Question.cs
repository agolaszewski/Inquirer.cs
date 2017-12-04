using System;
using System.Collections.Generic;
using InquirerCS.Builders;
using InquirerCS.Questions;

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

        public static InputStructBuilder<TResult> Input<TResult>(string message) where TResult : struct
        {
            return new InputStructBuilder<TResult>(message);
        }

        ////public static Listing<TResult> Listing<TResult>(string message, IEnumerable<TResult> choices) where TResult : IComparable
        ////{
        ////    var choicesList = choices.ToList();
        ////    var convertToString = new ConvertToStringComponent<TResult>();
        ////    var msgComponent = new message(message);

        ////    var confirmComponent = new NoConfirmationComponent<TResult>();
        ////    var defaultComponent = new DefaultListValueComponent<TResult>();

        ////    var displayQuestionComponent = new DisplayQuestion<TResult>(msgComponent, convertToString, defaultComponent);
        ////    var inputComponent = new ReadConsoleKey();
        ////    var parseComponent = new ParseListComponent<TResult>(choicesList);
        ////    var displayChoices = new DisplayChoices<TResult>(choicesList, convertToString);
        ////    var validationComponent = new ValidationComponent<TResult>();
        ////    var errorDisplay = new DisplayErrorCompnent();

        ////    return new Listing<TResult>(choicesList, confirmComponent, displayQuestionComponent, inputComponent, parseComponent, displayChoices, validationComponent, errorDisplay);
        ////}

        ////public static Input<string> Password(string message)
        ////{
        ////    var convertToString = new ConvertToStringComponent<string>();

        ////    var msgComponent = new message(message);

        ////    var defaultComponent = new DefaultValueComponent<string>();

        ////    var displayQuestionComponent = new DisplayQuestion<string>(msgComponent, convertToString, defaultComponent);
        ////    var inputComponent = new HideReadStringComponent();
        ////    var parseComponent = new ParseComponent<string, string>(value =>
        ////    {
        ////        return value;
        ////    });
        ////    var confirmComponent = new ConfirmPasswordComponent(inputComponent);

        ////    var validationInputComponent = new ValidationComponent<string>();
        ////    validationInputComponent.AddValidator(value => { return string.IsNullOrEmpty(value) == false || defaultComponent.HasDefaultValue; }, "Empty line");

        ////    var validationResultComponent = new ValidationComponent<string>();
        ////    var errorDisplay = new DisplayErrorCompnent();

        ////    return new Input<string>(confirmComponent, displayQuestionComponent, inputComponent, parseComponent, validationResultComponent, validationInputComponent, errorDisplay, defaultComponent);
        ////}

        ////public static RawList<TResult> RawList<TResult>(string message, IEnumerable<TResult> choices) where TResult : IComparable
        ////{
        ////    var choicesList = choices.ToList();
        ////    var convertToString = new ConvertToStringComponent<TResult>();
        ////    var msgComponent = new message(message);

        ////    var confirmComponent = new NoConfirmationComponent<TResult>();
        ////    var defaultComponent = new DefaultListValueComponent<TResult>();

        ////    var displayQuestionComponent = new DisplayQuestion<TResult>(msgComponent, convertToString, defaultComponent);
        ////    var inputComponent = new ReadStringComponent();

        ////    var parseComponent = new ParseComponent<string, TResult>(value =>
        ////    {
        ////        return choicesList[value.To<int>()];
        ////    });

        ////    var displayChoices = new DisplayChoices<TResult>(choicesList, convertToString);

        ////    var validationResultComponent = new ValidationComponent<TResult>();

        ////    var validationInputComponent = new ValidationComponent<string>();
        ////    validationInputComponent.AddValidator(value => { return string.IsNullOrEmpty(value) == false || defaultComponent.HasDefaultValue; }, "Empty line");
        ////    validationInputComponent.AddValidator(value => { return value.ToN<int>().HasValue; }, value => { return $"Cannot parse {value} to {typeof(TResult)}"; });
        ////    validationInputComponent.AddValidator(
        ////    value =>
        ////    {
        ////        var index = value.To<int>();
        ////        return index > 0 && index <= choicesList.Count;
        ////    },
        ////    value =>
        ////    {
        ////        return $"Choosen number must be between 1 and {choicesList.Count}";
        ////    });

        ////    var errorDisplay = new DisplayErrorCompnent();

        ////    return new RawList<TResult>(choicesList, confirmComponent, displayQuestionComponent, inputComponent, parseComponent, displayChoices, validationResultComponent, validationInputComponent, errorDisplay);
        ////}
    }
}