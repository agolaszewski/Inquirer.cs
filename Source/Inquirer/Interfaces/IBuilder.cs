namespace InquirerCS.Interfaces
{
    public interface IBuilder<TResult>
    {
        TResult Prompt();
    }
}
