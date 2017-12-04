using System;
using System.Collections.Generic;
using System.Linq;
using InquirerCS.Components;
using InquirerCS.Questions;

namespace InquirerCS.Builders
{
    public class RawListBuilder<TResult> : Builder<string, TResult> where TResult : IComparable
    {
        private List<TResult> _choices;

        private DisplayChoices<TResult> _displayChoices;

        private string _message;

        public RawListBuilder(string message, IEnumerable<TResult> choices)
        {
            _message = message;
            _choices = choices.ToList();
        }

        public override TResult Build()
        {
            _convertToString = new ConvertToStringComponent<TResult>();

            _confirmComponent = new NoConfirmationComponent<TResult>();
            _defaultComponent = new DefaultListValueComponent<TResult>();

            _displayQuestionComponent = new DisplayQuestion<TResult>(_msgComponent, _convertToString, _defaultComponent);
            _inputComponent = new ReadStringComponent();

            _parseComponent = new ParseComponent<string, TResult>(value =>
            {
                return _choices[value.To<int>()];
            });

            _displayChoices = new DisplayChoices<TResult>(_choices, _convertToString);

            _validationResultComponent = new ValidationComponent<TResult>();

            var validationInputComponent = new ValidationComponent<string>();
            validationInputComponent.AddValidator(value => { return string.IsNullOrEmpty(value) == false || _defaultComponent.HasDefaultValue; }, "Empty line");
            validationInputComponent.AddValidator(value => { return value.ToN<int>().HasValue; }, value => { return $"Cannot parse {value} to {typeof(TResult)}"; });
            validationInputComponent.AddValidator(
            value =>
            {
                var index = value.To<int>();
                return index > 0 && index <= _choices.Count;
            },
            value =>
            {
                return $"Choosen number must be between 1 and {_choices.Count}";
            });

            var errorDisplay = new DisplayErrorCompnent();

            return new RawList<TResult>(_choices, _confirmComponent, _displayQuestionComponent, _inputComponent, _parseComponent, _displayChoices, _validationResultComponent, validationInputComponent, errorDisplay).Prompt();
        }
    }
}
