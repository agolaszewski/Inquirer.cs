using System;

namespace InquirerCS.Beta
{
    public class ConfirmComponent<TResult> : IConfirmComponent<TResult>
    {
        private IConvertToStringComponent<TResult> _convertToStringComponent;

        public ConfirmComponent(IConvertToStringComponent<TResult> convertToStringComponent)
        {
            _convertToStringComponent = convertToStringComponent;
        }

        public bool Run(TResult answer)
        {
            Console.Clear();
            ConsoleHelper.WriteLine($"Are you sure? [y/n] : {_convertToStringComponent.Convert(answer)} ");
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
                return false;
            }

            return true;
        }
    }
}