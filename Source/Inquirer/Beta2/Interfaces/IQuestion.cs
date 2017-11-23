namespace InquirerCS.Beta2.Interfaces
{
    public interface IQuestion<TResult>
    {
        TResult Prompt();
    }
}