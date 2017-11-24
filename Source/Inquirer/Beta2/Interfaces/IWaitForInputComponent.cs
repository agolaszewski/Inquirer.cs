namespace InquirerCS.Beta2.Interfaces
{
    public interface IWaitForInputComponent<TInput>
    {
        TInput WaitForInput();
    }
}