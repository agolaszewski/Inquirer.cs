namespace InquirerCS.Interfaces
{
    public interface IWaitForInputComponent<TInput>
    {
        TInput WaitForInput();
    }
}
