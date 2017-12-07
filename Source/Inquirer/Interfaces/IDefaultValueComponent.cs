namespace InquirerCS.Interfaces
{
    public interface IDefaultValueComponent<TResult>
    {
        TResult DefaultValue { get; }

        bool HasDefaultValue { get; }
    }
}
