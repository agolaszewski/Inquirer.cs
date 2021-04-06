using System;
using System.Linq.Expressions;
using System.Reflection;
using InquirerCS.Components;
using InquirerCS.Interfaces;
using InquirerCS.Traits;

namespace InquirerCS
{
    public class LazyNode<TBuilder, TQuestion, TResult> : BaseNode where TBuilder : IWaitForInputTrait<StringOrKey>, IOnKeyTrait, IBuilder<TQuestion, TResult> where TQuestion : IQuestion<TResult>
    {
        private Func<TBuilder> _fn;
        private Action<TResult> _then;
        private Action _after;

        public LazyNode(Func<TBuilder> fn) : base()
        {
            _fn = fn;
            History.CurrentScope.Add(this);
        }

        public override bool Run()
        {
            var node = CreateNode(_fn());
            return node.Run();
        }

        public IThen Then(Action<TResult> then)
        {
            _then = then;
            return new ThenComponent(this);
        }

        public BaseNode CreateNode(TBuilder builder)
        {
            if (builder != null)
            {
                var node = new Node<TBuilder, TQuestion, TResult>(builder, addHistory: false);
                node.Then(_then);
                node.After(_after);
                return node;
            }

            return new EmptyNode();
        }

        public virtual void Bind(Expression<Func<TResult>> action)
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
        }

        internal override void After(Action after)
        {
            _after = after;
        }
    }
}