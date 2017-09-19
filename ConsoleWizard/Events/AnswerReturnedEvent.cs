namespace ConsoleWizard.Events
{
    public class AnswerReturnedEvent<T> : IEvent
    {
        public AnswerReturnedEvent(T answer)
        {
            Answer = answer;
        }

        private T Answer { get; set; }
    }
}