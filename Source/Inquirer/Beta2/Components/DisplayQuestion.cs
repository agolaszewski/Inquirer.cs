using System;
using InquirerCS.Beta2.Interfaces;

namespace InquirerCS.Beta2.Components
{
    public class DisplayQuestion<TResult> : IRenderComponent
    {
        private IConvertToStringComponent<TResult> _convertToStringComponent;
        private IDefaultValueComponent<TResult> _defaultValueComponent;
        private IMessageComponent _messageComponent;

        public DisplayQuestion(IMessageComponent messageComponent, IConvertToStringComponent<TResult> convertToStringComponent, IDefaultValueComponent<TResult> defaultValueComponent)
        {
            _convertToStringComponent = convertToStringComponent ?? new ConvertToStringComponent<TResult>();
            _defaultValueComponent = defaultValueComponent ?? new DefaultValueComponent<TResult>();

            if (messageComponent == null)
            {
                throw new ArgumentNullException("messageComponent");
            }
            else
            {
                _messageComponent = messageComponent;
            }
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