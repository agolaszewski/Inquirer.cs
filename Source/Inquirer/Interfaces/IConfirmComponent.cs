namespace InquirerCS.Interfaces
{
    public interface IConfirmComponent<TResult>
    {
        bool Confirm(TResult result);
    }
}
