namespace InquirerCS.Beta
{
    public class ConfirmEntity<TResult> : IConfirmEntity
    {
        private IConfirmComponent<TResult> _confirmComponent;
        private TResult _result;

        public ConfirmEntity(TResult result, IConfirmComponent<TResult> confirmComponent)
        {
            _result = result;
            _confirmComponent = confirmComponent;
        }

        public bool Run()
        {
            return _confirmComponent.Run(_result);
        }
    }
}