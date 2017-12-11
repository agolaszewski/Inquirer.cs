﻿using System;
using InquirerCS.Interfaces;
using InquirerCS.Traits;

namespace InquirerCS.Components
{
    internal class DisplaPagedRawChoices<TResult> : IRenderChoices<TResult>
    {
        private const int _CURSOR_OFFSET = 2;

        private IConvertToStringTrait<TResult> _convert;

        private IPagingTrait<TResult> _paging;

        public DisplaPagedRawChoices(IPagingTrait<TResult> paging, IConvertToStringTrait<TResult> convert)
        {
            _paging = paging;
            _convert = convert;
        }

        public void Render()
        {
            int index = 0;
            foreach (TResult choice in _paging.Paging.CurrentPage)
            {
                ConsoleHelper.PositionWriteLine($"[{index + 1}] {_convert.Convert.Run(choice)}", 0, index + _CURSOR_OFFSET);
                index++;
            }
        }

        public void Select(int index)
        {
            ConsoleHelper.PositionWriteLine($"[{index + 1}] {_convert.Convert.Run(_paging.Paging.CurrentPage[index])}", 0, index + _CURSOR_OFFSET, ConsoleColor.DarkYellow);
        }
    }
}