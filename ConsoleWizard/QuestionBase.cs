using System;

namespace ConsoleWizard
{
    public abstract class QuestionBase<TAnswer>
    {
        public string Message { get; set; }

        public QuestionBase(string message)
        {
            Message = message;
        }

        public TAnswer Answer { get; set; }

        internal bool HasDefaultValue { get; set; }
        public TAnswer DefaultValue { get; set; }

        internal bool HasConfirmation { get; set; }

        public Func<TAnswer, string> ToStringFn { get; set; } = value => { return value.ToString(); };

        protected bool Confirm(TAnswer result)
        {
            if (HasConfirmation)
            {
                Console.Clear();
                Console.WriteLine($"Are you sure? [y/n] : {ToStringFn(result)} ");
                ConsoleKeyInfo key = default(ConsoleKeyInfo);
                do
                {
                    key = Console.ReadKey();
                    Console.SetCursorPosition(0, Console.CursorTop);
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
            ConsoleHelper.Write("[?] ", ConsoleColor.Yellow);
            var question = $"{Message} : ";
            if (HasDefaultValue)
            {
                question += $"[{ToStringFn(DefaultValue)}] ";
            }

            ConsoleHelper.Write(question);
        }

        public abstract TAnswer Prompt();
    }
}