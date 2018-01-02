namespace InquirerCS.Interfaces
{
    public interface IBuilder<TQuestion, TResult>
    {
        TQuestion Build();

        TResult Prompt();
    }
}
