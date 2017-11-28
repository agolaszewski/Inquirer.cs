using InquirerCS.Beta2.Interfaces;

namespace InquirerCS.Beta2.Components
{
    public class NoConfirmationComponent<TResult> : IConfirmComponent<TResult>
    {
        public bool Confirm(TResult result)
        {
            return false;
        }
    }
}