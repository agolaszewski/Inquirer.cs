using InquirerCS.Interfaces;

namespace InquirerCS.Components
{
    public class DefaultValueComponent<TResult> : IDefaultValueComponent<TResult>
    {
        public DefaultValueComponent()
        {
            HasDefault = false;
            Value = default(TResult);
        }

        public DefaultValueComponent(TResult defaultValue)
        {
            HasDefault = true;
            Value = defaultValue;
        }

        public bool HasDefault { get; }

        public TResult Value { get; }
    }
}
