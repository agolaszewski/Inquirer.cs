using System;
using System.Collections.Generic;
using InquirerCS.Interfaces;

namespace InquirerCS.Components
{
    public class DisplaySelectableChoices<TResult> : IRenderChoices<TResult>
    {
        private const int _CURSOR_OFFSET = 2;

        private List<Selectable<TResult>> _choices;

        private IConvertToStringComponent<TResult> _convertToString;

        public DisplaySelectableChoices(List<Selectable<TResult>> choices, IConvertToStringComponent<TResult> convertToString)
        {
            _choices = choices;
            _convertToString = convertToString;
        }

        public void Render()
        {
            int index = 0;
            foreach (Selectable<TResult> choice in _choices)
            {
                ConsoleHelper.PositionWriteLine($"     {_convertToString.Convert(choice.Item)}", 0, index + _CURSOR_OFFSET);
                ConsoleHelper.PositionWriteLine(choice.IsSelected ? "*" : " ", 3, index + _CURSOR_OFFSET);
                index++;
            }
        }

        public void Select(int index)
        {
            ConsoleHelper.PositionWriteLine($"->   {_convertToString.Convert(_choices[index].Item)}", 0, index + _CURSOR_OFFSET, ConsoleColor.DarkYellow);
            ConsoleHelper.PositionWriteLine(_choices[index].IsSelected ? "*" : " ", 3, index + _CURSOR_OFFSET, ConsoleColor.DarkYellow);
        }
    }
}
