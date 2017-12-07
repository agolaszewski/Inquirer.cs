using InquirerCS.Interfaces;

namespace InquirerCS.Components
{
    public class DefaultValueComponent<TResult> : IDefaultValueComponent<TResult>
    {
        public DefaultValueComponent()
        {
            HasDefaultValue = false;
            DefaultValue = default(TResult);
        }

        public DefaultValueComponent(TResult defaultValue)
        {
            HasDefaultValue = true;
            DefaultValue = defaultValue;
        }

        public TResult DefaultValue { get; }

        public bool HasDefaultValue { get; }
    }
}
