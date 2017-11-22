namespace InquirerCS.Beta
{
    public interface IReadInputComponent<TInput>
    {
        TInput WaitForInput();
    }
}