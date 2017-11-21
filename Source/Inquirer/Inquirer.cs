using System;
using System.Collections.Generic;
using System.Linq;

namespace InquirerCS
{
    public class Inquirer
    {
        private Stack<Action> History { get; set; } = new Stack<Action>();

        public TResult Prompt<TResult>(QuestionSingleChoiceBase<ConsoleKey, TResult> question)
        {
            question.ReadFn = () =>
            {
                bool isCanceled;
                var read = ConsoleHelper.ReadKey(out isCanceled);
                if (isCanceled)
                {
                    question.IsCanceled = true;
                    throw new OperationCanceledException();
                }

                return read;
            };
            return question.Prompt();
        }

        public TResult Prompt<TResult>(QuestionSingleChoiceBase<string, TResult> question)
        {
            question.ReadFn = () =>
            {
                ConsoleKey? isCanceled;
                var read = ConsoleHelper.Read(out isCanceled, ConsoleKey.Escape);
                if (isCanceled.HasValue)
                {
                    question.IsCanceled = true;
                    throw new OperationCanceledException();
                }

                return read;
            };
            return question.Prompt();
        }

        public void Next(Action action)
        {
            try
            {
                action.Invoke();
                History.Push(action);
            }
            catch (OperationCanceledException)
            {
                if (History.Any())
                {
                    Next(History.Pop());
                }
            }
        }
    }
}