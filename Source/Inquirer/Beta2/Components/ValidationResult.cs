using System;
using InquirerCS.Beta2.Interfaces;

namespace InquirerCS.Beta2.Components
{
    public class ValidationResult : IValidationResult
    {
        private Func<object, string> item2;

        public ValidationResult()
        {
            HasError = false;
        }

        public ValidationResult(string errorMessage)
        {
            HasError = true;
            ErrorMessage = errorMessage;
        }

        public string ErrorMessage { get; }

        public bool HasError { get; }
    }
}