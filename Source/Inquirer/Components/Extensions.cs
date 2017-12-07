using System;
using InquirerCS.Components;
using InquirerCS.Interfaces;

namespace InquirerCS.Components
{
    public class Extensions<TResult>
    {
        public Func<IConfirmComponent<TResult>> ConfirmComponentFn = () => { return null; };

        public Func<IConvertToStringComponent<TResult>> ConvertToStringComponentFn = () => { return null; };

        public Func<IDefaultValueComponent<TResult>> DefaultValueComponentFn = () => { return null; };

        public IValidateComponent<TResult> Validators = new ValidationComponent<TResult>();

        public IConfirmComponent<TResult> Confirm { get; set; }

        public IConvertToStringComponent<TResult> Convert { get; set; }

        public IDefaultValueComponent<TResult> Default { get; set; }

        public void Build()
        {
            Convert = ConvertToStringComponentFn() ?? new ConvertToStringComponent<TResult>();
            Default = DefaultValueComponentFn() ?? new DefaultValueComponent<TResult>();
            Confirm = ConfirmComponentFn() ?? new NoConfirmationComponent<TResult>();
        }
    }
}