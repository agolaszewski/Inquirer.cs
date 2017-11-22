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

        public List<TResult> Prompt<TResult>(QuestionMultipleListBase<List<TResult>, TResult> question)
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

        public TResult Prompt<TResult>(QuestionSingleChoiceBase<ConsoleKey, ConsoleKey, TResult> question)
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

        public TResult Prompt<TResult>(QuestionSingleChoiceBase<ConsoleKey, int?, TResult> question)
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

        public TResult Prompt<TResult>(QuestionSingleChoiceBase<ConsoleKey, string, TResult> question)
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

        public TResult Prompt<TResult>(QuestionSingleChoiceBase<string, int?, TResult> question)
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

        public TResult Prompt<TResult>(QuestionSingleChoiceBase<string, string, TResult> question)
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
    }
}
