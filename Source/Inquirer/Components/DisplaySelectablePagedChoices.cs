using System;
using InquirerCS.Interfaces;

namespace InquirerCS.Components
{
    public class DisplaySelectablePagedChoices<TResult> : IRenderChoices<TResult>
    {
        private const int _CURSOR_OFFSET = 2;

        private IConvertToStringComponent<TResult> _convertToStringComponent;

        private IPagingComponent<Selectable<TResult>> _pagingComponent;

        public DisplaySelectablePagedChoices(IPagingComponent<Selectable<TResult>> pagingComponent, IConvertToStringComponent<TResult> convertToString)
        {
            _pagingComponent = pagingComponent;
            _convertToStringComponent = convertToString;
        }

        public void Render()
        {
            int index = 0;
            foreach (var choice in _pagingComponent.CurrentPage)
            {
                ConsoleHelper.PositionWriteLine($"     {_convertToStringComponent.Run(choice.Item)}", 0, index + _CURSOR_OFFSET);
                ConsoleHelper.PositionWriteLine(choice.IsSelected ? "*" : " ", 3, index + _CURSOR_OFFSET);
                index++;
            }
        }

        public void Select(int index)
        {
            ConsoleHelper.PositionWriteLine($"->   {_convertToStringComponent.Run(_pagingComponent.CurrentPage[index].Item)}", 0, index + _CURSOR_OFFSET, ConsoleColor.DarkYellow);
            ConsoleHelper.PositionWriteLine(_pagingComponent.CurrentPage[index].IsSelected ? "*" : " ", 3, index + _CURSOR_OFFSET, ConsoleColor.DarkYellow);
        }
    }
}
