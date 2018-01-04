using System;
using System.Collections.Generic;
using InquirerCS.Interfaces;

namespace InquirerCS
{
    public abstract class InquirerProgram
    {
        private static Stack<Action> _history = new Stack<Action>();

        public static void Does(Action does)
        {
            does.Invoke();
            _history.Push(does);
        }

        public static void Pop()
        {
            Does(_history.Pop());
        }

        public static TResult Does<TQuestion, TResult>(IBuilder<TQuestion, TResult> builder)
        {
            return builder.Prompt();
        }

        public InquirerProgram When()
        {
            return this;
        }
    }
}