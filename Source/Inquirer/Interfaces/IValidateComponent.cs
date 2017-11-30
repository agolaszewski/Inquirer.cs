using System;

namespace InquirerCS.Interfaces
{
    public interface IValidateComponent<T>
    {
        IValidateComponent<T> AddValidator(Func<T, bool> fn, Func<T, string> errorMessageFn);

        IValidateComponent<T> AddValidator(Func<T, bool> fn, string errorMessage);

        IValidationResult Run(T value);
    }
}
