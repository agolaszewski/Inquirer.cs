using System;

namespace InquirerCS.Beta
{
    public class UIInput<TResult> : IUIComponent
    {
        private IConvertToStringComponent<TResult> _convertToStringComponent;
        private IDefaultValueComponent<TResult> _defaultValueComponent;
        private IMessageComponent _messageComponent;

        public UIInput(IMessageComponent messageComponent, IConvertToStringComponent<TResult> convertToStringComponent, IDefaultValueComponent<TResult> defaultValueComponent)
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

        public virtual void Render()
        {
            ConsoleHelper.Write("[?] ", ConsoleColor.Yellow);
            var question = $"{_messageComponent.Message} : ";
            if (_defaultValueComponent.HasDefaultValue)
            {
                question += $"[{_convertToStringComponent.Convert(_defaultValueComponent.DefaultValue)}] ";
            }

            Console.Write(question);
        }

        public void Render<T>(IValidateComponent<T> validationComponent)
        {
            Render();
            if (validationComponent.HasError)
            {
                Console.WriteLine();
                ConsoleHelper.WriteError(validationComponent.ErrorMessage);
                Console.ReadKey();
            }
        }
    }
}