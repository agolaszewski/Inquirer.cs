using System;
using System.Linq.Expressions;
using System.Reflection;
using InquirerCS.Components;
using InquirerCS.Interfaces;
using InquirerCS.Traits;

namespace InquirerCS
{
    public class Node<TBuilder, TQuestion, TResult> : BaseNode where TBuilder : IWaitForInputTrait<StringOrKey>, IOnKeyTrait, IBuilder<TQuestion, TResult> where TQuestion : IQuestion<TResult>
    {
        private Action _after;

        private TBuilder _builder;

        private Action<TResult> _then;

        internal Node()
        {
        }

        internal Node(TBuilder builder, bool addHistory = true)
        {
            _builder = builder;
            _builder.Input.IntteruptedKeys.Add(ConsoleKey.Escape);
            _builder.OnKey = new OnEscape();

            if (addHistory)
            {
                History.CurrentScope.Add(this);
            }
        }

        public virtual IThen Bind(Expression<Func<TResult>> action)
        {
            if (((MemberExpression)action.Body).Member is PropertyInfo)
            {
                var propertyInfo = ((MemberExpression)action.Body).Member as PropertyInfo;

                var body = action.Body as MemberExpression;
                object projection = Expression.Lambda<Func<object>>(body.Expression).Compile()();

                _then = answer => { propertyInfo.SetValue(projection, answer); };
            }

            if (((MemberExpression)action.Body).Member is FieldInfo)
            {
                var fieldInfo = ((MemberExpression)action.Body).Member as FieldInfo;

                var body = action.Body as MemberExpression;
                object projection = Expression.Lambda<Func<object>>(body.Expression).Compile()();

                _then = answer => { fieldInfo.SetValue(projection, answer); };
            }

            return new ThenComponent(this);
        }

        public override bool Run()
        {
            var answer = _builder.Build().Prompt();
            if (_builder.OnKey.IsInterrupted)
            {
                return false;
            }

            History.Push();
            _then?.Invoke(answer);
            _after?.Invoke();
            History.Pop();
            return true;
        }

        public virtual IThen Then(Action<TResult> toBind)
        {
            _then = answer => { toBind(answer); History.Process(History.CurrentScope.Current); };
            return new ThenComponent(this);
        }

        public virtual IThen Then(TResult toBind)
        {
            _then = answer => { toBind = answer; };
            return new ThenComponent(this);
        }

        internal override void After(Action after)
        {
            _after = after;
        }
    }
}