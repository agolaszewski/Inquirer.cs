using System;
using System.Collections.Generic;
using System.Linq;
using InquirerCS.Interfaces;
using InquirerCS.Traits;

namespace InquirerCS.Components
{
    public class DisplayChoices<TResult> : IRenderChoices<TResult>
    {
        private const int _CURSOR_OFFSET = 2;

        private List<TResult> _choices;

        private IConvertToStringTrait<TResult> _convert;

        public DisplayChoices(List<TResult> choices, IConvertToStringTrait<TResult> convert)
        {
            _choices = choices;
            _convert = convert;
        }

        public void Render()
        {
            int index = 0;
            foreach (TResult choice in _choices)
            {
                ConsoleHelper.PositionWriteLine($"   {_convert.Convert.Run(choice)}", 0, index + _CURSOR_OFFSET);
                index++;
            }
        }

        public void Select(int index)
        {
            ConsoleHelper.PositionWriteLine($"-> {_convert.Convert.Run(_choices.ElementAt(index))}", 0, index + _CURSOR_OFFSET, ConsoleColor.DarkYellow);
        }
    }
}
