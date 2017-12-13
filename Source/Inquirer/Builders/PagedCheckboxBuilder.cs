using System;
using System.Collections.Generic;
using InquirerCS.Components;
using InquirerCS.Interfaces;
using InquirerCS.Questions;
using InquirerCS.Traits;

namespace InquirerCS.Builders
{
    public class PagedCheckboxBuilder<TResult>
        : IConfirmTrait<List<TResult>>,
        IConvertToStringTrait<TResult>,
        IDefaultTrait<List<TResult>>,
        IValidateResultTrait<List<TResult>>,
        IRenderQuestionTrait,
        IRenderChoicesTrait<TResult>,
        IDisplayErrorTrait,
        IWaitForInputTrait<StringOrKey>,
        IParseTrait<Dictionary<int, List<Selectable<TResult>>>, List<TResult>>,
        IOnKeyTrait,
        IPagingTrait<Selectable<TResult>> where TResult : IComparable
    {
        private List<Selectable<TResult>> _choices;

        private IConsole _console;

        public PagedCheckboxBuilder(IConsole console)
        {
            _console = console;

            this.Confirm(this, _console);
            this.ConvertToString();
            this.Default();
            this.ResultValidate();
            this.Input(_console, ConsoleKey.Spacebar, ConsoleKey.UpArrow, ConsoleKey.DownArrow, ConsoleKey.LeftArrow, ConsoleKey.RightArrow, ConsoleKey.Enter);
            this.OnKey();
        }

        public PagedCheckboxBuilder(string message, List<Selectable<TResult>> choices, int pageSize, IConsole console) : this(console)
        {
            _choices = choices;

            this.Paging(choices, pageSize);
            this.RenderQuestion(message, this, this, _console);
            this.RenderChoices(this, this, _console);
            this.Parse(this);
        }

        public IConfirmComponent<List<TResult>> Confirm { get; set; }

        public IConvertToStringComponent<TResult> Convert { get; set; }

        public IDefaultValueComponent<List<TResult>> Default { get; set; }

        public IDisplayErrorComponent DisplayError { get; set; }

        public IWaitForInputComponent<StringOrKey> Input { get; set; }

        public IOnKey OnKey { get; set; }

        public IPagingComponent<Selectable<TResult>> Paging { get; set; }

        public IParseComponent<Dictionary<int, List<Selectable<TResult>>>, List<TResult>> Parse { get; set; }

        public IRenderChoices<TResult> RenderChoices { get; set; }

        public IRenderQuestionComponent RenderQuestion { get; set; }

        public IValidateComponent<List<TResult>> ResultValidators { get; set; }

        public PagedCheckbox<List<TResult>, TResult> Build()
        {
            return new PagedCheckbox<List<TResult>, TResult>(Paging, Confirm, RenderQuestion, Input, Parse, RenderChoices, ResultValidators, DisplayError, OnKey);
        }

        public virtual PagedCheckboxBuilder<TResult> WithConfirmation()
        {
            this.Confirm(this, _console);
            return this;
        }

        public virtual PagedCheckboxBuilder<TResult> WithConvertToString(Func<TResult, string> fn)
        {
            this.ConvertToString(fn);
            return this;
        }

        public virtual PagedCheckboxBuilder<TResult> WithDefaultValue(List<TResult> defaultValues)
        {
            this.Default(_choices, defaultValues);
            return this;
        }

        public virtual PagedCheckboxBuilder<TResult> WithDefaultValue(TResult defaultValue)
        {
            this.Default(_choices, new List<TResult>() { defaultValue });
            return this;
        }

        public PagedCheckboxBuilder<TResult> WithValidation(Func<List<TResult>, bool> fn, Func<List<TResult>, string> errorMessageFn)
        {
            ResultValidators.Add(fn, errorMessageFn);
            return this;
        }

        public PagedCheckboxBuilder<TResult> WithValidation(Func<List<TResult>, bool> fn, string errorMessage)
        {
            ResultValidators.Add(fn, errorMessage);
            return this;
        }
    }
}
