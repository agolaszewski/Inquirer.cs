using InquirerCS.Interfaces;

namespace InquirerCS.Components
{
    public class NoConfirmationComponent<TResult> : IConfirmComponent<TResult>
    {
        public bool Confirm(TResult result)
        {
            return false;
        }
    }
}
