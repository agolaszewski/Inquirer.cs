using System;
using System.Collections.Generic;
using ConsoleWizard.Components;

namespace ConsoleWizard
{
    internal class DisplayQuestionMultipleChoiceComponent<TQuestion, TList, TType> where TList : List<TType> where TQuestion : QuestionBase<TList>, IConvertToString<TType>
    {
        private TQuestion _questionBase;

        public DisplayQuestionMultipleChoiceComponent(TQuestion questionBase)
        {
            _questionBase = questionBase;
        }

        public void DisplayQuestion()
        {
            Console.Clear();
            ConsoleHelper.Write("[?] ", ConsoleColor.Yellow);
            var question = $"{_questionBase.Message} : ";
            if (_questionBase.HasDefaultValue)
            {
                foreach (var item in _questionBase.DefaultValue)
                {
                    question += $"[{_questionBase.ConvertToStringFn(item)}] ";
                }
            }
        }
    }
}