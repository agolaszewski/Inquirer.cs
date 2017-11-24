namespace InquirerCS.Beta2.Interfaces
{
    public interface IConfirmComponent<TResult>
    {
        bool Run(TResult result);
    }
}