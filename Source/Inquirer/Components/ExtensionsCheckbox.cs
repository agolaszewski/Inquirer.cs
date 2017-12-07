using System;
using System.Collections.Generic;
using InquirerCS.Interfaces;

namespace InquirerCS.Components
{
    public class ExtensionsCheckbox<TResult>
    {
        public Func<IConfirmComponent<List<TResult>>> ConfirmComponentFn = () => { return null; };

        public Func<IConvertToStringComponent<TResult>> ConvertToStringComponentFn = () => { return null; };

        public Func<IDefaultValueComponent<List<TResult>>> DefaultValueComponentFn = () => { return null; };

        public IValidateComponent<List<TResult>> Validators = new ValidationComponent<List<TResult>>();

        public IConfirmComponent<List<TResult>> Confirm { get; set; }

        public IConvertToStringComponent<TResult> Convert { get; set; }

        public IDefaultValueComponent<List<TResult>> Default { get; set; }

        public void Build()
        {
            Convert = ConvertToStringComponentFn() ?? new ConvertToStringComponent<TResult>();
            Default = DefaultValueComponentFn() ?? new DefaultValueComponent<List<TResult>>();
            Confirm = ConfirmComponentFn() ?? new NoConfirmationComponent<List<TResult>>();
        }
    }
}
