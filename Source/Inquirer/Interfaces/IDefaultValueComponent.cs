namespace InquirerCS.Interfaces
{
    public interface IDefaultValueComponent<TResult>
    {
        TResult Value { get; }

        bool HasDefault { get; }
    }
}
