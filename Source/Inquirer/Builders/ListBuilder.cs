using System;
using System.Collections.Generic;
using System.Linq;
using InquirerCS.Components;
using InquirerCS.Interfaces;
using InquirerCS.Questions;

namespace InquirerCS.Builders
{
    public abstract class ListBuilder<TResult, TInput, TCollection> where TResult : IComparable
    {
        protected TCollection _choicesDictionary;
        protected IConfirmComponent<TResult> _confirmComponent;
        protected IConvertToStringComponent<TResult> _convertToString;
        protected IDefaultValueComponent<TResult> _defaultComponent;
        protected IRenderChoices<TResult> _displayChoices;
        protected IDisplayQuestionComponent _displayQuestionComponent;
        protected IDisplayErrorComponent _errorDisplay;
        protected IWaitForInputComponent<TInput> _inputComponent;
        protected string _msgComponent;
        protected IParseComponent<ConsoleKey, TResult> _parseComponent;
        protected IValidateComponent<ConsoleKey> _validationInputComponent;
        protected IValidateComponent<TResult> _validationResultComponent;

        protected Func<IConfirmComponent<TResult>> _confirmComponentFn;
        protected Func<IDefaultValueComponent<TResult>> _defaultValueComponentFn;
        protected Func<IConvertToStringComponent<TResult>> _convertToStringFn;

        public abstract TResult Prompt();
    }
}