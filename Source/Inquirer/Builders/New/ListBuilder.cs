using System;
using System.Collections.Generic;
using System.Linq;
using InquirerCS.Components;
using InquirerCS.Interfaces;
using InquirerCS.Questions;
using InquirerCS.Traits;

namespace InquirerCS.Builders.New
{
    public class ListBuilder<TResult> : InputBuilder<Listing<TResult>, int, TResult>, IRenderChoicesTrait<TResult> where TResult : IComparable
    {
        private List<TResult> _choices;

        public ListBuilder(string message, IEnumerable<TResult> choices)
        {
            _choices = choices.ToList();

            this.RenderQuestion(message, this, this);
            this.Parse(_choices);
            this.RenderChoices(_choices, this);
            this.Parse(_choices);
        }

        public IRenderChoices<TResult> RenderChoices { get; set; }

        public override InputBuilder<Listing<TResult>, int, TResult> WithDefaultValue(TResult defaultValue)
        {
            Default = new DefaultListValueComponent<TResult>(_choices, defaultValue);
            return this;
        }

        public override Listing<TResult> Build()
        {
            return new Listing<TResult>(_choices, Confirm, RenderQuestion, Input, Parse, RenderChoices, ResultValidators, DisplayError, OnKey);
        }
    }
}