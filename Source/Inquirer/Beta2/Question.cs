using System;
using System.Collections.Generic;
using InquirerCS.Beta2.Components;
using InquirerCS.Beta2.Questions;

namespace InquirerCS.Beta2
{
    public static class Question
    {
        public static Checkbox<List<TResult>, TResult> Checkbox<TResult>(string message, IEnumerable<TResult> choices)
        {
            var messageComponent = new MessageComponent(message);
            var convertToStringComponent = new ConvertToStringComponent<TResult>();
            var defaultValueComponent = new DefaultValueComponent<List<TResult>>();

            var displayQuestionComponent = new DisplayListQuestion<List<TResult>, TResult>(messageComponent, convertToStringComponent, defaultValueComponent);
            var choicesComponent = new SelectableListChoices<TResult>(choices);

            var confirmComponent = new NoConfirmationComponent<List<TResult>>();
            var inputComponent = new ReadConsoleKey();
            var parseComponent = new ParseSelectableListComponent<List<TResult>, TResult>(choicesComponent);
            var renderChoicesComponent = new DisplaySelectableChoices<TResult>(choicesComponent, convertToStringComponent);
            var validateComponent = new ValidationComponent<List<TResult>>();
            var errorComponent = new DisplayErrorCompnent();

            return new Checkbox<List<TResult>, TResult>(choicesComponent, confirmComponent, displayQuestionComponent, inputComponent, parseComponent, renderChoicesComponent, validateComponent, errorComponent);
        }

        public static Listing<TResult> Listing<TResult>(string message, IEnumerable<TResult> choices) where TResult : IComparable
        {
            var choicesComponent = new ListChoices<TResult>(choices);
            var convertToString = new ConvertToStringComponent<TResult>();
            var msgComponent = new MessageComponent(message);

            var confirmComponent = new NoConfirmationComponent<TResult>();
            var defaultComponent = new DefaultListValueComponent<TResult>();

            var displayQuestionComponent = new DisplayQuestion<TResult>(msgComponent, convertToString, defaultComponent);
            var inputComponent = new ReadConsoleKey();
            var parseComponent = new ParseListComponent<TResult>(choicesComponent);
            var displayChoices = new DisplayChoices<TResult>(choicesComponent, convertToString);
            var validationComponent = new ValidationComponent<TResult>();
            var errorDisplay = new DisplayErrorCompnent();

            return new Listing<TResult>(choicesComponent, confirmComponent, displayQuestionComponent, inputComponent, parseComponent, displayChoices, validationComponent, errorDisplay);
        }

        public static InputKey<bool> Confirm(string message)
        {
            var convertToString = new ConvertToStringComponent<bool>(value =>
            {
                return value ? "yes" : "no";
            });

            var msgComponent = new MessageComponent(message + " [y/n]");

            var confirmComponent = new NoConfirmationComponent<bool>();
            var defaultComponent = new DefaultValueComponent<bool>();

            var displayQuestionComponent = new DisplayQuestion<bool>(msgComponent, convertToString, defaultComponent);
            var inputComponent = new ReadConsoleKey();
            var parseComponent = new ParseComponent<ConsoleKey, bool>(value =>
            {
                return value == ConsoleKey.Y;
            });

            var validationInputComponent = new ValidationComponent<ConsoleKey>();
            var validationResultComponent = new ValidationComponent<bool>();
            var errorDisplay = new DisplayErrorCompnent();

            return new InputKey<bool>(confirmComponent, displayQuestionComponent, inputComponent, parseComponent, validationResultComponent, validationInputComponent, errorDisplay, defaultComponent);
        }

        public static Input<string> Input(string message)
        {
            var convertToString = new ConvertToStringComponent<string>();

            var msgComponent = new MessageComponent(message);

            var confirmComponent = new NoConfirmationComponent<string>();
            var defaultComponent = new DefaultValueComponent<string>();

            var displayQuestionComponent = new DisplayQuestion<string>(msgComponent, convertToString, defaultComponent);
            var inputComponent = new ReadStringComponent();
            var parseComponent = new ParseComponent<string, string>(value =>
            {
                return value;
            });

            var validationInputComponent = new ValidationComponent<string>();
            validationInputComponent.AddValidator(value => { return string.IsNullOrEmpty(value) == false || defaultComponent.HasDefaultValue; }, "Empty line");

            var validationResultComponent = new ValidationComponent<string>();
            var errorDisplay = new DisplayErrorCompnent();

            return new Input<string>(confirmComponent, displayQuestionComponent, inputComponent, parseComponent, validationResultComponent, validationInputComponent, errorDisplay, defaultComponent);
        }

        public static Input<TResult> Input<TResult>(string message) where TResult : struct
        {
            var convertToString = new ConvertToStringComponent<TResult>();

            var msgComponent = new MessageComponent(message);

            var confirmComponent = new NoConfirmationComponent<TResult>();
            var defaultComponent = new DefaultValueComponent<TResult>();

            var displayQuestionComponent = new DisplayQuestion<TResult>(msgComponent, convertToString, defaultComponent);
            var inputComponent = new ReadStringComponent();
            var parseComponent = new ParseComponent<string, TResult>(value =>
            {
                return value.To<TResult>();
            });

            var validationInputComponent = new ValidationComponent<string>();
            validationInputComponent.AddValidator(value => { return string.IsNullOrEmpty(value) == false || defaultComponent.HasDefaultValue; }, "Empty line");
            validationInputComponent.AddValidator(value => { return value.ToN<TResult>().HasValue; }, value => { return $"Cannot parse {value} to {typeof(TResult)}"; });

            var validationResultComponent = new ValidationComponent<TResult>();
            var errorDisplay = new DisplayErrorCompnent();

            return new Input<TResult>(confirmComponent, displayQuestionComponent, inputComponent, parseComponent, validationResultComponent, validationInputComponent, errorDisplay, defaultComponent);
        }
    }
}