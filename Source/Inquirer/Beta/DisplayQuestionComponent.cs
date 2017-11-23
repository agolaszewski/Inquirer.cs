using System;

namespace InquirerCS.Beta
{
    public class DisplayQuestionComponent<TResult> : IDisplayQuestionComponent
    {
        private IConvertToStringComponent<TResult> _convertToStringComponent;
        private IDefaultValueComponent<TResult> _defaultValueComponent;
        private IMessageComponent _messageComponent;

        public DisplayQuestionComponent(IMessageComponent messageComponent, IConvertToStringComponent<TResult> convertToStringComponent, IDefaultValueComponent<TResult> defaultValueComponent)
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

        public void Run()
        {
            
        }
    }
}