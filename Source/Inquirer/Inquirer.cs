using System;
using System.Collections.Generic;

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
    }
}
