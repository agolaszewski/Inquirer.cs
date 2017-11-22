namespace InquirerCS.Beta
{
    public interface IDefaultValueComponent<TResult>
    {
        TResult DefaultValue { get; }

        bool HasDefaultValue { get; }
    }
}