namespace InquirerCS.Beta2.Interfaces
{
    public interface IValidationResult
    {
        string ErrorMessage { get; }

        bool HasError { get; }
    }
}
