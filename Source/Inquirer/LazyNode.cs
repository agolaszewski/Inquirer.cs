using System;
using InquirerCS.Components;
using InquirerCS.Interfaces;
using InquirerCS.Traits;

namespace InquirerCS
{
    public class LazyNode<TBuilder, TQuestion, TResult> : BaseNode where TBuilder : IWaitForInputTrait<StringOrKey>, IOnKeyTrait, IBuilder<TQuestion, TResult> where TQuestion : IQuestion<TResult>
    {
        private Func<TBuilder> _fn;
        private Action<TResult> _toBind;

        public LazyNode(Func<TBuilder> fn) : base()
        {
            _fn = fn;
            History.CurrentScope.Add(this);
        }

        public override bool Run()
        {
            var node = Inquirer.Prompt<TBuilder, TQuestion, TResult>(_fn());
            node.Then(_toBind);
            return node.Run();
        }

        public void Then(Action<TResult> toBind)
        {
            _toBind = toBind;
        }
    }
}