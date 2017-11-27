using System;
using InquirerCS.Beta2.Interfaces;

namespace InquirerCS.Beta2.Components
{
    public class ConfirmComponent<TResult> : IConfirmComponent<TResult>
    {
        private IConvertToStringComponent<TResult> _convertToStringComponent;

        public ConfirmComponent(IConvertToStringComponent<TResult> convertToStringComponent)
        {
            _convertToStringComponent = convertToStringComponent;
        }

        public bool Confirm(TResult result)
        {
            Console.Clear();
            ConsoleHelper.WriteLine($"Are you sure? [y/n] : {_convertToStringComponent.Convert(result)} ");
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

            return false;
        }
    }
}