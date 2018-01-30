using System;
using InquirerCS.Interfaces;
using InquirerCS.Traits;

namespace InquirerCS.Components
{
    internal class DisplayPagedListChoices<TResult> : IRenderChoices<TResult>
    {
        private IConsole _console;

        private IConvertToStringTrait<TResult> _convert;

        private IPagingTrait<TResult> _paging;

        public DisplayPagedListChoices(IPagingTrait<TResult> paging, IConvertToStringTrait<TResult> convert, IConsole console)
        {
            _paging = paging;
            _convert = convert;
            _console = console;
        }

        public void Render()
        {
            int index = 0;
            foreach (TResult choice in _paging.Paging.CurrentPage)
            {
                _console.PositionWriteLine($"   {_convert.Convert.Run(choice)}", 0, index + Consts.CURSOR_OFFSET);
                index++;
            }

            _console.WriteLine(" ");
            _console.WriteLine($"Page {_paging.Paging.CurrentPageNumber + 1} of {_paging.Paging.PagedChoices.Count}");
        }

        public void Select(int index)
        {
            _console.PositionWriteLine($"-> {_convert.Convert.Run(_paging.Paging.CurrentPage[index])}", 0, index + Consts.CURSOR_OFFSET, ConsoleColor.DarkYellow);
        }

        public void UnSelect(int index)
        {
            _console.PositionWriteLine($"   {_convert.Convert.Run(_paging.Paging.CurrentPage[index])}", 0, index + Consts.CURSOR_OFFSET);
        }
    }
}
