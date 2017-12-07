using System;
using InquirerCS.Interfaces;

namespace InquirerCS.Components
{
    internal class DisplaPagedRawChoices<TResult> : IRenderChoices<TResult>
    {
        private const int _CURSOR_OFFSET = 2;

        private IConvertToStringComponent<TResult> _convertToStringComponent;

        private IPagingComponent<TResult> _pagingComponent;

        public DisplaPagedRawChoices(IPagingComponent<TResult> pagingComponent, IConvertToStringComponent<TResult> convertToString)
        {
            _pagingComponent = pagingComponent;
            _convertToStringComponent = convertToString;
        }

        public void Render()
        {
            int index = 0;
            foreach (TResult choice in _pagingComponent.CurrentPage)
            {
                ConsoleHelper.PositionWriteLine($"[{index + 1}] {_convertToStringComponent.Convert(choice)}", 0, index + _CURSOR_OFFSET);
                index++;
            }
        }

        public void Select(int index)
        {
            ConsoleHelper.PositionWriteLine($"[{index + 1}] {_convertToStringComponent.Convert(_pagingComponent.CurrentPage[index])}", 0, index + _CURSOR_OFFSET, ConsoleColor.DarkYellow);
        }
    }
}
