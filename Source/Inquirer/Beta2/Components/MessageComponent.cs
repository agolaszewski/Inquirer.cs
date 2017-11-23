using InquirerCS.Beta2.Interfaces;

namespace InquirerCS.Beta2.Components
{
    public class MessageComponent : IMessageComponent
    {
        public MessageComponent(string message)
        {
            Message = message;
        }

        public string Message { get; }
    }
}