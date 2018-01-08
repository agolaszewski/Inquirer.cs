using System;
using System.Collections.Generic;
using InquirerCS.Components;
using InquirerCS.Interfaces;
using InquirerCS.Traits;

namespace InquirerCS
{
    public class Inquirer
    {
        internal Stack<Action> History { get; set; } = new Stack<Action>();

        public InquirerMenu Menu(string header)
        {
            return new InquirerMenu(header, this);
        }

        public void Next(Action action)
        {
            try
            {
                History.Push(action);
                action.Invoke();
            }
            catch (OperationCanceledException)
            {
                if (History.Count > 1)
                {
                    History.Pop();
                    Next(History.Pop());
                }
                else
                {
                    Next(History.Pop());
                }
            }
        }

        public TResult Prompt<TQuestion, TResult>(IBuilder<TQuestion, TResult> builder)
        {
            if (builder is IWaitForInputTrait<StringOrKey>)
            {
                var waitForInputTrait = builder as IWaitForInputTrait<StringOrKey>;
                waitForInputTrait.Input.IntteruptedKeys.Add(ConsoleKey.Escape);
            }

            if (builder is IOnKeyTrait)
            {
                var waitForInputTrait = builder as IOnKeyTrait;
                waitForInputTrait.OnKey = new OnEscape();
            }

            return builder.Prompt();
        }
    }
}
