using System;
using System.Collections.Generic;
using InquirerCS.Interfaces;

namespace InquirerCS.Builders
{
    internal class DisplaRawChoices<TResult> : IRenderChoices<TResult>
    {
        private const int _CURSOR_OFFSET = 2;

        private List<TResult> _choices;

        private IConvertToStringComponent<TResult> _convertToStringComponent;

        public DisplaRawChoices(List<TResult> choices, IConvertToStringComponent<TResult> convertToString)
        {
            _choices = choices;
            _convertToStringComponent = convertToString;
        }

        public void Render()
        {
            int index = 0;
            foreach (TResult choice in _choices)
            {
                ConsoleHelper.PositionWriteLine($"[{index + 1}] {_convertToStringComponent.Run(choice)}", 0, index + _CURSOR_OFFSET);
                index++;
            }
        }

        public void Select(int index)
        {
            ConsoleHelper.PositionWriteLine($"[{index + 1}] {_convertToStringComponent.Run(_choices[index])}", 0, index + _CURSOR_OFFSET, ConsoleColor.DarkYellow);
        }
    }
}
