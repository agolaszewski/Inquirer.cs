using InquirerCS.Interfaces;

namespace InquirerCS.Components
{
    internal class NoConfirmationComponent<TResult> : IConfirmComponent<TResult>
    {
        public bool Confirm(TResult result)
        {
            return false;
        }
    }
}
