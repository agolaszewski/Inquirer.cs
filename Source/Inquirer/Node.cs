using System;
using InquirerCS.Components;
using InquirerCS.Interfaces;
using InquirerCS.Traits;

namespace InquirerCS
{
    public class Node<TBuilder, TQuestion, TResult> : BaseNode where TBuilder : IWaitForInputTrait<StringOrKey>, IOnKeyTrait, IBuilder<TQuestion, TResult> where TQuestion : IQuestion<TResult>
    {
        private TBuilder _builder;

        private Action<TResult> _then;

        public Node(TBuilder builder)
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
                if (History.Stack.Count != 0)
                {
                    History.Stack.Pop().Run();
                }
                else
                {
                    Run();
                }
            }
            else
            {
                History.Stack.Push(this);
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
