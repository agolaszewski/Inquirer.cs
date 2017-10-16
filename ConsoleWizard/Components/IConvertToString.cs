using System;

namespace ConsoleWizard.Components
{
    public interface IConvertToString<TIn>
    {
        Func<TIn, string> ToStringFn { get; set; }
    }
}