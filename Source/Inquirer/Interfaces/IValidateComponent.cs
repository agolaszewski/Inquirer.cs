using System;

namespace InquirerCS.Interfaces
{
    public interface IValidateComponent<T>
    {
        IValidateComponent<T> Add(Func<T, bool> fn, Func<T, string> errorMessageFn);

        IValidateComponent<T> Add(Func<T, bool> fn, string errorMessage);

        IValidationResult Run(T value);
    }
}
