using System;
using InquirerCS.Interfaces;
using InquirerCS.Traits;

namespace InquirerCS.Components
{
    public class DisplayPagedListChoices<TResult> : IRenderChoices<TResult>
    {
        private const int _CURSOR_OFFSET = 2;

        private IConvertToStringTrait<TResult> _convert;

        private IPagingTrait<TResult> _paging;

        public DisplayPagedListChoices(IPagingTrait<TResult> paging, IConvertToStringTrait<TResult> convert)
        {
            _paging = paging;
            _convert = convert;
        }

        public void Render()
        {
            int index = 0;
            foreach (TResult choice in _paging.Paging.CurrentPage)
            {
                AppConsole2.PositionWriteLine($"   {_convert.Convert.Run(choice)}", 0, index + _CURSOR_OFFSET);
                index++;
            }
        }

        public void Select(int index)
        {
            AppConsole2.PositionWriteLine($"-> {_convert.Convert.Run(_paging.Paging.CurrentPage[index])}", 0, index + _CURSOR_OFFSET, ConsoleColor.DarkYellow);
        }
    }
}
