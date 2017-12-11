using System;
using InquirerCS.Interfaces;
using InquirerCS.Traits;

namespace InquirerCS.Components
{
    public class DisplaySelectablePagedChoices<TResult> : IRenderChoices<TResult>
    {
        private const int _CURSOR_OFFSET = 2;

        private IConvertToStringTrait<TResult> _convert;
        private IPagingTrait<Selectable<TResult>> _paging;

        public DisplaySelectablePagedChoices(IPagingTrait<Selectable<TResult>> paging, IConvertToStringTrait<TResult> convert)
        {
            _paging = paging;
            _convert = convert;
        }

        public void Render()
        {
            int index = 0;
            foreach (var choice in _paging.Paging.CurrentPage)
            {
                ConsoleHelper.PositionWriteLine($"     {_convert.Convert.Run(choice.Item)}", 0, index + _CURSOR_OFFSET);
                ConsoleHelper.PositionWriteLine(choice.IsSelected ? "*" : " ", 3, index + _CURSOR_OFFSET);
                index++;
            }
        }

        public void Select(int index)
        {
            ConsoleHelper.PositionWriteLine($"->   {_convert.Convert.Run(_paging.Paging.CurrentPage[index].Item)}", 0, index + _CURSOR_OFFSET, ConsoleColor.DarkYellow);
            ConsoleHelper.PositionWriteLine(_paging.Paging.CurrentPage[index].IsSelected ? "*" : " ", 3, index + _CURSOR_OFFSET, ConsoleColor.DarkYellow);
        }
    }
}