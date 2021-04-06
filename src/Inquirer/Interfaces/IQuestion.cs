namespace InquirerCS.Interfaces
{
    public interface IQuestion<TResult>
    {
        TResult Prompt();
    }
}
