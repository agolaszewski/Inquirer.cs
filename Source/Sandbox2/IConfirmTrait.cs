using System;
using InquirerCS.Components;
using InquirerCS.Interfaces;

namespace InquirerCS.Traits
{
    public interface IConfirmTrait<TResult>
    {
        IConfirmComponent<TResult> Confirm { get; set; }
    }

    public interface IConvertToStringTrait<TResult>
    {
        IConvertToStringComponent<TResult> Convert { get; set; }
    }

    public class Test : IConfirmTrait<string>, IConvertToStringTrait<string>
    {
        public IConfirmComponent<string> Confirm { get; set; }

        public IConvertToStringComponent<string> Convert { get; set; }

        public Test()
        {
            this.Confirmmm(this);
            this.Derp();
            Confirm.Confirm("Adsada");
        }
    }

    public class TestCon<TResult> : IConfirmComponent<TResult>
    {
        private IConvertToStringTrait<TResult> _trait;

        public TestCon(IConvertToStringTrait<TResult> trait)
        {
            _trait = trait;
        }

        public bool Confirm(TResult result)
        {
            Console.WriteLine(_trait.Convert.Convert(result));
            return false;
        }
    }

    public static class Ext
    {
        public static void Confirmmm(this IConfirmTrait<string> trait, IConvertToStringTrait<string> derp)
        {
            trait.Confirm = new TestCon<string>(derp);
        }

        public static void Derp(this IConvertToStringTrait<string> trait)
        {
            trait.Convert = new ConvertToStringComponent<string>(x => { return "test"; });
        }
    }
}