using System;
using System.Collections.Generic;
using System.Linq;
using InquirerCS.Interfaces;

namespace InquirerCS.Components
{
    public class DisplayChoices<TResult> : IRenderChoices<TResult>
    {
        private const int _CURSOR_OFFSET = 2;

        private List<TResult> _choices;

        private IConvertToStringComponent<TResult> _convertToStringComponent;

        public DisplayChoices(List<TResult> choices, IConvertToStringComponent<TResult> convertToString)
        {
            _choices = choices;
            _convertToStringComponent = convertToString;
        }

        public void Render()
        {
            int index = 0;
            foreach (TResult choice in _choices)
            {
                ConsoleHelper.PositionWriteLine($"   {_convertToStringComponent.Convert(choice)}", 0, index + _CURSOR_OFFSET);
                index++;
            }
        }

        public void Select(int index)
        {
            ConsoleHelper.PositionWriteLine($"-> {_convertToStringComponent.Convert(_choices.ElementAt(index))}", 0, index + _CURSOR_OFFSET, ConsoleColor.DarkYellow);
        }
    }
}
