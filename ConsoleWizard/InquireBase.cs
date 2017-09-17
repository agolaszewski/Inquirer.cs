using System;

namespace ConsoleWizard
{
    public abstract class InquireBase<T>
    {
        public InquireBase(string question)
        {
            WithOptions = new ConsoleWizardFluentInterface<T>(this);
            Question = question;
        }

        public ConsoleWizardFluentInterface<T> WithOptions { get; private set; }
        public string Question { get; private set; }

        internal bool HasDefaultValue { get; set; }
        internal T DefaultValue { get; set; }

        public T Answer { get; internal set; }
        internal bool HasConfirmation { get; set; }

        public Action<T> ResultFn { get; internal set; } = v => { };

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

        protected void DisplayQuestion()
        {
            var question = $"[?] {Question} : ";
            if (HasDefaultValue)
            {
                question += $"[{DefaultValue.ToString()}] ";
            }

            Console.Write(question);
        }

        public abstract T Prompt();
    }
}