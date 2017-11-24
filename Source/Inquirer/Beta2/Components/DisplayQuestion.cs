using System;
using InquirerCS.Beta2.Interfaces;

namespace InquirerCS.Beta2.Components
{
    public class DisplayQuestion<TResult> : IRenderComponent
    {
        private IConvertToStringComponent<TResult> _convertToStringComponent;
        private IDefaultValueComponent<TResult> _defaultValueComponent;
        private IMessageComponent _messageComponent;

        public DisplayQuestion()
        {
        }

        public void Register(IMessageComponent messageComponent)
        {
            _messageComponent = messageComponent;
        }

        public void Register(IConvertToStringComponent<TResult> convertToStringComponent)
        {
            _convertToStringComponent = convertToStringComponent;
        }

        public void Register(IDefaultValueComponent<TResult> defaultValueComponent)
        {
            _defaultValueComponent = defaultValueComponent;
        }

        public void Render()
        {
            Console.Clear();
            ConsoleHelper.Write("[?] ", ConsoleColor.Yellow);
            var question = $"{_messageComponent.Message} : ";
            if (_defaultValueComponent.HasDefaultValue)
            {
                question += $"[{_convertToStringComponent.Convert(_defaultValueComponent.DefaultValue)}] ";
            }

            Console.Write(question);
        }
    }
}