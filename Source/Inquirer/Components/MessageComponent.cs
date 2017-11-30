using InquirerCS.Interfaces;

namespace InquirerCS.Components
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
