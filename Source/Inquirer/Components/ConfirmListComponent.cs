using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InquirerCS.Interfaces;

namespace InquirerCS.Components
{
    public class ConfirmListComponent<TList, TResult> : IConfirmComponent<TList> where TList : IEnumerable<TResult>
    {
        private IConvertToStringComponent<TResult> _convertToStringComponent;

        public ConfirmListComponent(IConvertToStringComponent<TResult> convertToStringComponent)
        {
            _convertToStringComponent = convertToStringComponent;
        }

        public bool Confirm(TList result)
        {
            Console.Clear();

            StringBuilder sb = new StringBuilder();

            sb.Append($"Are you sure? [y/n] : ");

            sb.Append("[");
            sb.Append(string.Join(", ", result.Select(item => _convertToStringComponent.Convert(item))));
            sb.Append("]");

            ConsoleHelper.WriteLine(sb.ToString());
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
