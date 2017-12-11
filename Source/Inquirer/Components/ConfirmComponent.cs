using System;
using InquirerCS.Interfaces;
using InquirerCS.Traits;

namespace InquirerCS.Components
{
    public class ConfirmComponent<TResult> : IConfirmComponent<TResult>
    {
        private IConvertToStringTrait<TResult> _convert;

        public ConfirmComponent(IConvertToStringTrait<TResult> convert)
        {
            _convert = convert;
        }

        public bool Confirm(TResult result)
        {
            Console.Clear();
            ConsoleHelper.WriteLine($"Are you sure? [y/n] : {_convert.Convert.Run(result)} ");
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
