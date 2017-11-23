namespace InquirerCS.Beta2.Interfaces
{
    public interface IDefaultValueComponent<TResult>
    {
        TResult DefaultValue { get; }

        bool HasDefaultValue { get; }
    }
}