using System;
using InquirerCS.Interfaces;
using InquirerCS.Traits;

namespace InquirerCS.Components
{
    public class DisplaySelectablePagedChoices<TResult> : IRenderChoices<TResult>
    {
        private const int _CURSOR_OFFSET = 2;

        private IConsole _console;

        private IConvertToStringTrait<TResult> _convert;

        private IPagingTrait<Selectable<TResult>> _paging;

        public DisplaySelectablePagedChoices(IPagingTrait<Selectable<TResult>> paging, IConvertToStringTrait<TResult> convert, IConsole console)
        {
            _paging = paging;
            _convert = convert;
            _console = console;
        }

        public void Render()
        {
            int index = 0;
            foreach (var choice in _paging.Paging.CurrentPage)
            {
                _console.PositionWriteLine($"     {_convert.Convert.Run(choice.Item)}", 0, index + _CURSOR_OFFSET);
                _console.PositionWriteLine(choice.IsSelected ? "*" : " ", 3, index + _CURSOR_OFFSET);
                index++;
            }
        }

        public void Select(int index)
        {
            _console.PositionWriteLine($"->   {_convert.Convert.Run(_paging.Paging.CurrentPage[index].Item)}", 0, index + _CURSOR_OFFSET, ConsoleColor.DarkYellow);
            _console.PositionWriteLine(_paging.Paging.CurrentPage[index].IsSelected ? "*" : " ", 3, index + _CURSOR_OFFSET, ConsoleColor.DarkYellow);
        }
    }
}
