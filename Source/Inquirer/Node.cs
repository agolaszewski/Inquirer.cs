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

        internal Node(TBuilder builder)
        {
            _builder = builder;
            _builder.Input.IntteruptedKeys.Add(ConsoleKey.Escape);
            _builder.OnKey = new OnEscape();
        }

        public override void Run(bool back = false)
        {
            if (!back)
            {
                History.Push(this);
            }

            var answer = _builder.Build().Prompt();
            if (_builder.OnKey.IsInterrupted)
            {
                History.Pop(this).Run(true);
            }
            else
            {
                _then(answer);
                BaseNode nextNode = History.Next(this);
                if (nextNode != null)
                {
                    nextNode.Run(true);
                }
            }
        }

        public void Then(Action<TResult> toBind)
        {
            _then = toBind;
            History.Scope += 1;
            Run();
            //History.Scope -= 1;
        }

        public void Then(ref TResult toBind)
        {
            TResult temp = toBind;
            _then = answer => { temp = answer; };
            History.Scope += 1;
            Run();
            //History.Scope -= 1;
            toBind = temp;
        }

        public void Then(TResult toBind)
        {
            TResult temp = toBind;
            _then = answer => { temp = answer; };
            History.Scope += 1;
            Run();
            History.Scope -= 1;
            toBind = temp;
        }
    }
}