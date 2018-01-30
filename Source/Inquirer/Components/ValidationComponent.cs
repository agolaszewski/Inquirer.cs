using System;
using System.Collections.Generic;
using InquirerCS.Interfaces;

namespace InquirerCS.Components
{
    internal class ValidationComponent<T> : IValidateComponent<T>
    {
        private List<Tuple<Func<T, bool>, Func<T, string>>> _validators = new List<Tuple<Func<T, bool>, Func<T, string>>>();

        public IValidateComponent<T> Add(Func<T, bool> fn, Func<T, string> errorMessageFn)
        {
            _validators.Add(new Tuple<Func<T, bool>, Func<T, string>>(fn, errorMessageFn));
            return this;
        }

        public IValidateComponent<T> Add(Func<T, bool> fn, string errorMessage)
        {
            _validators.Add(new Tuple<Func<T, bool>, Func<T, string>>(fn, value => { return errorMessage; }));
            return this;
        }

        public IValidationResult Run(T value)
        {
            foreach (var validator in _validators)
            {
                if (!validator.Item1(value))
                {
                    return new ValidationResult(validator.Item2(value));
                }
            }

            return new ValidationResult();
        }
    }
}
