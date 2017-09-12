using System;

namespace BetterConsole
{
    public abstract class InquireBase
    {
        public string Question { get; set; }
        public abstract void Prompt();
    }

    public abstract class InquireBase<T> : InquireBase
    {
        public T DefaultValue { get; internal set; }
        public bool HasDefaultValue { get; internal set; }
        public bool HasConfirmation { get; internal set; }
        public Action<T> NavigateFn { get; internal set; } = v => { };
        public Action<T> ResultFn { get; internal set; } = v => { };

        public InquireBase<T> HasAnswer(Action<T> p)
        {
            ResultFn = p;
            return this;
        }

        protected bool Confirm(T result)
        {
            if (HasConfirmation)
            {
                Console.WriteLine($"Are you sure? [y/n] : {result.ToString()} ");
                ConsoleKeyInfo key = default(ConsoleKeyInfo);
                do
                {
                    key = Console.ReadKey();
                }
                while (key.Key != ConsoleKey.Y && key.Key != ConsoleKey.N);

                if (key.Key == ConsoleKey.N)
                {
                    Console.WriteLine();
                    return true;
                }
            }

            return false;
        }

        public InquireBase<T> WithDefault(T defaultValue)
        {
            HasDefaultValue = true;
            DefaultValue = defaultValue;

            return this;
        }

        public InquireBase<T> WithConfirmation()
        {
            HasConfirmation = true;
            return this;
        }

        public InquireBase<T> Navigate(Action navigateFn)
        {
            NavigateFn = x => { navigateFn(); };
            return this;
        }

        public InquireBase<T> Navigate(Action<T> navigateFn)
        {
            NavigateFn = navigateFn;
            return this;
        }

        public InquireBase<T> Navigate(InquireBase navigateTo)
        {
            NavigateFn = x => { navigateTo.Prompt(); };
            return this;
        }

        public InquireBase<T> Navigate<TBase>(InquireBase<TBase> navigateTo)
        {
            NavigateFn = x => { navigateTo.Prompt(); };
            return this;
        }

        protected void DisplayQuestion()
        {
            var question = $"[?] {Question} : ";
            if (HasDefaultValue)
            {
                question += $"[{DefaultValue.ToString()}] ";
            }

            Console.Write(question);
        }
    }
}