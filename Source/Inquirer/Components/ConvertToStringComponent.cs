using System;
using InquirerCS.Interfaces;

namespace InquirerCS.Components
{
    public class ConvertToStringComponent<TResult> : IConvertToStringComponent<TResult>
    {
        public ConvertToStringComponent(Func<TResult, string> convertToStringFn = null)
        {
            if (convertToStringFn != null)
            {
                Run = convertToStringFn;
            }
        }

        public Func<TResult, string> Run { get; } = value => { return value.ToString(); };
    }
}
