using System;

namespace ConsoleWizard
{
    public abstract class QuestionBase<TAnswer>
    {
        public QuestionBase(string message)
        {
            Message = message;
        }

        public string Message { get; set; }

        internal bool HasDefaultValue { get; set; }
        internal TAnswer DefaultValue { get; set; }

        public TAnswer Answer { get; set; }
        internal bool HasConfirmation { get; set; }

        protected bool Confirm(TAnswer result)
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
            ConsoleHelper.Write("[?] ", ConsoleColor.Yellow);
            var question = $"{Message} : ";
            if (HasDefaultValue)
            {
                question += $"[{DefaultValue.ToString()}] ";
            }

            Console.Write(question);
        }

        public abstract TAnswer Prompt();
    }
}