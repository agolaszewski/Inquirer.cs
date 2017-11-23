namespace InquirerCS.Beta
{
    public interface IConfirmComponent<TResult>
    {
        bool Run(TResult result);
    }
}