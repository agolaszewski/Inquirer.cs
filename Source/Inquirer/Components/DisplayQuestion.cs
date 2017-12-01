using System;
using InquirerCS.Interfaces;

namespace InquirerCS.Components
{
    public class DisplayQuestion<TResult> : IDisplayQuestionComponent
    {
        private IConvertToStringComponent<TResult> _convertToStringComponent;

        private IDefaultValueComponent<TResult> _defaultValueComponent;

        private string _message;

        public DisplayQuestion(string message, IConvertToStringComponent<TResult> convertToStringComponent, IDefaultValueComponent<TResult> defaultValueComponent)
        {
            _message = message;
            _convertToStringComponent = convertToStringComponent;
            _defaultValueComponent = defaultValueComponent;
        }

        public void Render()
        {
            Console.Clear();
            ConsoleHelper.Write("[?] ", ConsoleColor.Yellow);
            var question = $"{_message} : ";
            if (_defaultValueComponent.HasDefaultValue)
            {
                question += $"[{_convertToStringComponent.Convert(_defaultValueComponent.DefaultValue)}] ";
            }

            Console.Write(question);
        }
    }
}
