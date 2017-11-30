using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InquirerCS.Interfaces;

namespace InquirerCS.Components
{
    public class DisplayListQuestion<TList, TResult> : IDisplayQuestionComponent where TList : IEnumerable<TResult>
    {
        private IConvertToStringComponent<TResult> _convertToStringComponent;

        private IDefaultValueComponent<TList> _defaultValueComponent;

        private IMessageComponent _messageComponent;

        public DisplayListQuestion(IMessageComponent messageComponent, IConvertToStringComponent<TResult> convertToStringComponent, IDefaultValueComponent<TList> defaultValueComponent)
        {
            _messageComponent = messageComponent;
            _convertToStringComponent = convertToStringComponent;
            _defaultValueComponent = defaultValueComponent;
        }

        public void Render()
        {
            StringBuilder sb = new StringBuilder();

            Console.Clear();
            ConsoleHelper.Write("[?] ", ConsoleColor.Yellow);

            sb.Append($"{_messageComponent.Message} : ");
            if (_defaultValueComponent.HasDefaultValue)
            {
                sb.Append("[");
                sb.Append(string.Join(", ", _defaultValueComponent.DefaultValue.Select(item => _convertToStringComponent.Convert(item))));
                sb.Append("]");
            }

            Console.Write(sb.ToString());
        }
    }
}
