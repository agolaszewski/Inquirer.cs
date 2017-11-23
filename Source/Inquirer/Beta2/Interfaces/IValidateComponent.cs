using System;

namespace InquirerCS.Beta2.Interfaces
{
    public interface IValidateComponent<T>
    {
        void Run(T value);

        IValidateComponent<T> AddValidator(Func<T, bool> fn, Func<T, string> errorMessageFn);

        IValidateComponent<T> AddValidator(Func<T, bool> fn, string errorMessage);
    }
}