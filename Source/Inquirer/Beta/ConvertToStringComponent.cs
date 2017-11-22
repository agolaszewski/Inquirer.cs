using System;

namespace InquirerCS.Beta
{
    public class ConvertToStringComponent<TResult> : IConvertToStringComponent<TResult>
    {
        public ConvertToStringComponent(Func<TResult, string> convertToStringFn = null)
        {
            if (convertToStringFn != null)
            {
                Convert = convertToStringFn;
            }
        }

        public Func<TResult, string> Convert { get; } = value => { return value.ToString(); };
    }
}