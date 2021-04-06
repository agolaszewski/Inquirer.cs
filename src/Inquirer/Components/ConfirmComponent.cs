using System;
using InquirerCS.Interfaces;
using InquirerCS.Traits;

namespace InquirerCS.Components
{
    internal class ConfirmComponent<TResult> : IConfirmComponent<TResult>
    {
        private IConsole _console;

        private IConvertToStringTrait<TResult> _convert;

        public ConfirmComponent(IConvertToStringTrait<TResult> convert, IConsole console)
        {
            _convert = convert;
            _console = console;
        }

        public bool Confirm(TResult result)
        {
            _console.Clear();
            _console.WriteLine($"Are you sure? [y/n] : {_convert.Convert.Run(result)}");
            ConsoleKeyInfo key = default(ConsoleKeyInfo);
            do
            {
                key = _console.ReadKey();
                _console.SetCursorPosition(0, _console.CursorTop);
            }
            while (key.Key != ConsoleKey.Y && key.Key != ConsoleKey.N && key.Key != ConsoleKey.Enter && key.Key != ConsoleKey.Escape);

            if (key.Key == ConsoleKey.N || key.Key == ConsoleKey.Escape)
            {
                return true;
            }

            return false;
        }
    }
}
