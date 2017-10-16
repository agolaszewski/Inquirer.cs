using System;

namespace ConsoleWizard.Components
{
    public interface IConvertToResult<TIn, TOut>
    {
        Func<TIn, TOut> ParseFn { get; set; }
    }
}