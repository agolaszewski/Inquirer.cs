namespace InquirerCS.Interfaces
{
    public interface IValidationResult
    {
        string ErrorMessage { get; }

        bool HasError { get; }
    }
}
