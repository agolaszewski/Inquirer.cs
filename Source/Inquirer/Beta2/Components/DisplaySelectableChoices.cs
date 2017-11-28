using System;
using InquirerCS.Beta2.Interfaces;

namespace InquirerCS.Beta2.Components
{
    public class DisplaySelectableChoices<TResult> : IRenderChoicesComponent<TResult>
    {
        private const int _CURSOR_OFFSET = 2;

        private IChoicesComponent<Selectable<TResult>> _choicesComponent;

        private IConvertToStringComponent<TResult> _convertToString;

        public DisplaySelectableChoices(IChoicesComponent<Selectable<TResult>> choicesComponent, IConvertToStringComponent<TResult> convertToString)
        {
            _choicesComponent = choicesComponent;
            _convertToString = convertToString;
        }

        public void Render()
        {
            int index = 0;
            foreach (Selectable<TResult> choice in _choicesComponent.Choices)
            {
                ConsoleHelper.PositionWriteLine($"     {_convertToString.Convert(choice.Item)}", 0, index + _CURSOR_OFFSET);
                ConsoleHelper.PositionWriteLine(choice.IsSelected ? "*" : " ", 3, index + _CURSOR_OFFSET);
                index++;
            }
        }

        public void Select(int index)
        {
            ConsoleHelper.PositionWriteLine($"->   {_convertToString.Convert(_choicesComponent.Choices[index].Item)}", 0, index + _CURSOR_OFFSET, ConsoleColor.DarkYellow);
            ConsoleHelper.PositionWriteLine(_choicesComponent.Choices[index].IsSelected ? "*" : " ", 3, index + _CURSOR_OFFSET, ConsoleColor.DarkYellow);
        }
    }
}