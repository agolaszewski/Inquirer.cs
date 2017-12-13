using System;
using System.Collections.Generic;
using InquirerCS.Interfaces;

namespace InquirerCS.Components
{
    internal class DisplayExtendedChoices<TResult> : IRenderChoices<TResult>
    {
        private const int _CURSOR_OFFSET = 2;

        private Dictionary<ConsoleKey, TResult> _choicesDictionary;

        private IConsole _console;

        private IConvertToStringComponent<TResult> _convertToStringComponent;

        public DisplayExtendedChoices(Dictionary<ConsoleKey, TResult> choicesDictionary, IConvertToStringComponent<TResult> convertToString, IConsole console)
        {
            _choicesDictionary = choicesDictionary;
            _convertToStringComponent = convertToString;
            _console = console;
        }

        public void Render()
        {
            int index = 0;
            foreach (var choice in _choicesDictionary)
            {
                _console.PositionWriteLine($"[{choice.Key}] {_convertToStringComponent.Run(choice.Value)}", 0, index + _CURSOR_OFFSET);
                index++;
            }
        }

        public void Select(int index)
        {
        }
    }
}
