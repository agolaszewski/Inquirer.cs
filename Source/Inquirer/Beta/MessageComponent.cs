namespace InquirerCS.Beta
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