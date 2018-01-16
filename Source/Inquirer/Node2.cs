using System;
using InquirerCS.Components;
using InquirerCS.Interfaces;
using InquirerCS.Traits;

namespace InquirerCS
{
    public class Node2<TBuilder, TQuestion, TResult> : BaseNode where TBuilder : IWaitForInputTrait<StringOrKey>, IOnKeyTrait, IBuilder<TQuestion, TResult> where TQuestion : IQuestion<TResult>
    {
        private Action<TResult> _then;

        private TBuilder _builder;

        public Node2(TBuilder builder)
        {
            _builder = builder;
            _builder.Input.IntteruptedKeys.Add(ConsoleKey.Escape);
            _builder.OnKey = new OnEscape();
        }

        public override void Run()
        {
            var answer = _builder.Build().Prompt();
            if (_builder.OnKey.IsInterrupted)
            {
            }
            else
            {
                _then(answer);
            }
        }

        public void Then(Action<TResult> then)
        {
            _then = then;
            Run();
        }

        public void Then(ref TResult toBind)
        {
            TResult temp = toBind;
            _then = answer => { temp = answer; };
            Run();
            toBind = temp;
        }

        public void Then(TResult toBind)
        {
            TResult temp = toBind;
            _then = answer => { temp = answer; };
            Run();
            toBind = temp;
        }
    }
}