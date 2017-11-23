namespace InquirerCS.Beta2.Interfaces
{
    internal interface IWaitForInputComponent<TInput>
    {
        TInput WaitForInput();
    }
}