using System;

namespace ConsoleWizard.Components
{
    public interface IValidation<TIn>
    {
        Func<TIn, bool> ValidatationFn { get; set; }
    }
}