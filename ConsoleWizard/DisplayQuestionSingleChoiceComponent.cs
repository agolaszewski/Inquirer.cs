using System;
using ConsoleWizard.Components;

namespace ConsoleWizard
{
    internal class DisplayQuestionSingleChoiceComponent<TQuestion, TTYpe> where TQuestion : QuestionBase<TTYpe>, IConvertToString<TTYpe>
    {
        private TQuestion _questionBase;

        public DisplayQuestionSingleChoiceComponent(TQuestion questionBase)
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
                question += $"[{_questionBase.ConvertToStringFn(_questionBase.DefaultValue)}] ";
            }
        }
    }
}
