namespace InquirerCS.Interfaces
{
    public interface IDefaultValueComponent<TResult>
    {
        bool HasDefault { get; }

        TResult Value { get; }
    }
}
