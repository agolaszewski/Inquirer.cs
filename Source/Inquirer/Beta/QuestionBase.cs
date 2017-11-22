namespace InquirerCS.Beta
{
    public abstract class QuestionBase<TResult>
    {
        public abstract TResult Prompt();
    }
}