using System;

namespace InquirerCS
{
    public abstract class QuestionBase<TAnswer>
    {
        protected QuestionBase(string message)
        {
            Message = message;
        }

        internal TAnswer DefaultValue { get; set; }

        internal bool HasConfirmation { get; set; }

        internal bool HasDefaultValue { get; set; }

        internal bool IsCanceled { get; set; }

        internal string Message { get; set; }

        public abstract TAnswer Prompt();

        protected virtual bool Confirm(string result)
        {
            if (HasConfirmation)
            {
                Console.Clear();
                ConsoleHelper.WriteLine($"Are you sure? [y/n] : {result} ");
                ConsoleKeyInfo key = default(ConsoleKeyInfo);
                do
                {
                    key = Console.ReadKey();
                    Console.SetCursorPosition(0, Console.CursorTop);
                }
                while (key.Key != ConsoleKey.Y && key.Key != ConsoleKey.N && key.Key != ConsoleKey.Enter && key.Key != ConsoleKey.Escape);

                if (key.Key == ConsoleKey.N || key.Key == ConsoleKey.Escape)
                {
                    Console.WriteLine();
                    return true;
                }
            }

            return false;
        }
    }
}
