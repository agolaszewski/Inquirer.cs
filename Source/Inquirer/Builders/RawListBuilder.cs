using System;
using System.Collections.Generic;
using System.Linq;
using InquirerCS.Interfaces;
using InquirerCS.Questions;
using InquirerCS.Traits;

namespace InquirerCS.Builders
{
    public class RawListBuilder<TResult> : InputBuilder<RawList<TResult>, string, TResult>, IRenderChoicesTrait<TResult> where TResult : IComparable
    {
        private List<TResult> _choices;

        public RawListBuilder(string message, IEnumerable<TResult> choices)
        {
            _choices = choices.ToList();

            this.RenderQuestion(message, this, this);
            this.Parse(value =>
            {
                return _choices[value.To<int>() - 1];
            });

            this.RenderRawChoices(_choices, this);

            InputValidators.Add(value => { return string.IsNullOrEmpty(value) == false || Default.HasDefault; }, "Empty line");
            InputValidators.Add(value => { return value.ToN<int>().HasValue; }, value => { return $"Cannot parse {value} to {typeof(TResult)}"; });
            InputValidators.Add(
            value =>
            {
                var index = value.To<int>();
                return index > 0 && index <= _choices.Count;
            },
            value =>
            {
                return $"Choosen number must be between 1 and {_choices.Count}";
            });
        }

        public IRenderChoices<TResult> RenderChoices { get; set; }

        public override RawList<TResult> Build()
        {
            return new RawList<TResult>(_choices, Confirm, RenderQuestion, Input, Parse, RenderChoices, ResultValidators, InputValidators, DisplayError, OnKey);
        }

        public override InputBuilder<RawList<TResult>, string, TResult> WithDefaultValue(TResult defaultValue)
        {
            this.Default(_choices, defaultValue);
            return this;
        }
    }
}
