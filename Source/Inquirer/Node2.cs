using System;
using InquirerCS.Components;
using InquirerCS.Interfaces;
using InquirerCS.Traits;

namespace InquirerCS
{
    public class Node2<TBuilder, TQuestion, TResult> : BaseNode where TBuilder : IWaitForInputTrait<StringOrKey>, IOnKeyTrait, IBuilder<TQuestion, TResult> where TQuestion : IQuestion<TResult>
    {
        private TBuilder _builder;

        public Node2(TBuilder builder)
        {
            _builder = builder;
            _builder.Input.IntteruptedKeys.Add(ConsoleKey.Escape);
            _builder.OnKey = new OnEscape();
        }

        public Action<TResult> Then { get; set; }

        public override void Run()
        {
            var answer = _builder.Build().Prompt();
            if (_builder.OnKey.IsInterrupted)
            {
            }
            else
            {
                Then(answer);
            }
        }
    }
}