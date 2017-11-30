using System;
using System.Collections.Generic;
using System.Linq;
using InquirerCS.Interfaces;

namespace InquirerCS.Components
{
    public class DisplayChoices<TResult> : IRenderchoices<TResult>
    {
        private const int _CURSOR_OFFSET = 2;

        private List<TResult> _choices;

        private IConvertToStringComponent<TResult> _convertToString;

        public DisplayChoices(List<TResult> choices, IConvertToStringComponent<TResult> convertToString)
        {
            _choices = choices;
            _convertToString = convertToString;
        }

        public void Render()
        {
            int index = 0;
            foreach (TResult choice in _choices)
            {
                ConsoleHelper.PositionWriteLine($"   {_convertToString.Convert(choice)}", 0, index + _CURSOR_OFFSET);
                index++;
            }
        }

        public void Select(int index)
        {
            ConsoleHelper.PositionWriteLine($"-> {_convertToString.Convert(_choices.ElementAt(index))}", 0, index + _CURSOR_OFFSET, ConsoleColor.DarkYellow);
        }
    }
}
