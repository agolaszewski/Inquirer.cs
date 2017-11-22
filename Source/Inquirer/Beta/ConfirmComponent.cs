using System;
using InquirerCS.Beta;

namespace InquirerCS.Beta
{
    public class ConfirmComponent<TResult> : IConfirmComponent<TResult>
    {
        public ConfirmComponent()
        {
        }

        public bool Run(TResult answer)
        {
            throw new NotImplementedException();
        }
    }
}