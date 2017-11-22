using System;

namespace InquirerCS.Beta
{
    public interface IValidateComponent<T>
    {
        string ErrorMessage { get; }

        bool HasError { get; }

        void Run(T value);

        IValidateComponent<T> AddValidator(Func<T, bool> fn, Func<T, string> errorMessageFn);

        IValidateComponent<T> AddValidator(Func<T, bool> fn, string errorMessage);
    }
}