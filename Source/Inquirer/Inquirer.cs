using System;
using System.Collections.Generic;
using InquirerCS.Builders;
using InquirerCS.Components;
using InquirerCS.Interfaces;
using InquirerCS.Questions;
using InquirerCS.Traits;

namespace InquirerCS
{
    public static class Inquirer
    {
        private static AppConsole _console;

        static Inquirer()
        {
            _console = new AppConsole();
        }

        public static Node<CheckboxBuilder<TResult>, Checkbox<List<TResult>, TResult>, List<TResult>> Prompt<TResult>(CheckboxBuilder<TResult> builder)
        {
            return Prompt<CheckboxBuilder<TResult>, Checkbox<List<TResult>, TResult>, List<TResult>>(builder);
        }

        public static LazyNode<CheckboxBuilder<TResult>, Checkbox<List<TResult>, TResult>, List<TResult>> Prompt<TResult>(Func<CheckboxBuilder<TResult>> fn)
        {
            return Prompt<CheckboxBuilder<TResult>, Checkbox<List<TResult>, TResult>, List<TResult>>(fn);
        }

        public static LazyNode<InputBuilder<TQuestion, TInput, TResult>, TQuestion, TResult> Prompt<TQuestion, TInput, TResult>(Func<InputBuilder<TQuestion, TInput, TResult>> fn) where TQuestion : IQuestion<TResult>
        {
            return Prompt<InputBuilder<TQuestion, TInput, TResult>, TQuestion, TResult>(fn);
        }

        public static LazyNode<PagedCheckboxBuilder<TResult>, PagedCheckbox<List<TResult>, TResult>, List<TResult>> Prompt<TResult>(Func<PagedCheckboxBuilder<TResult>> fn)
        {
            return Prompt<PagedCheckboxBuilder<TResult>, PagedCheckbox<List<TResult>, TResult>, List<TResult>>(fn);
        }

        public static LazyNode<TBuilder, TQuestion, TResult> Prompt<TBuilder, TQuestion, TResult>(Func<TBuilder> fn) where TBuilder : IWaitForInputTrait<StringOrKey>, IOnKeyTrait, IBuilder<TQuestion, TResult> where TQuestion : IQuestion<TResult>
        {
            return new LazyNode<TBuilder, TQuestion, TResult>(fn);
        }

        public static Node<InputBuilder<TQuestion, TInput, TResult>, TQuestion, TResult> Prompt<TQuestion, TInput, TResult>(InputBuilder<TQuestion, TInput, TResult> inputBuilder) where TQuestion : IQuestion<TResult>
        {
            return Prompt<InputBuilder<TQuestion, TInput, TResult>, TQuestion, TResult>(inputBuilder);
        }

        public static void Prompt(MenuBuilder builder)
        {
            var node = Prompt<MenuBuilder, ConsoleList<MenuAction>, MenuAction>(builder);
            node.Then(answer => answer.Action());
        }

        public static Node<PagedCheckboxBuilder<TResult>, PagedCheckbox<List<TResult>, TResult>, List<TResult>> Prompt<TResult>(PagedCheckboxBuilder<TResult> builder)
        {
            return Prompt<PagedCheckboxBuilder<TResult>, PagedCheckbox<List<TResult>, TResult>, List<TResult>>(builder);
        }

        public static Node<TBuilder, TQuestion, TResult> Prompt<TBuilder, TQuestion, TResult>(TBuilder builder) where TBuilder : IWaitForInputTrait<StringOrKey>, IOnKeyTrait, IBuilder<TQuestion, TResult> where TQuestion : IQuestion<TResult>
        {
            return new Node<TBuilder, TQuestion, TResult>(builder);
        }

        public static void Go()
        {
            History.Process(History.CurrentScope.Current);
        }
    }
}