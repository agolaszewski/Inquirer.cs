using System;
using System.Collections.Generic;
using InquirerCS.Interfaces;
using InquirerCS.Traits;

namespace InquirerCS.Components
{
    internal class DisplayChoices<TResult> : IRenderChoices<TResult>
    {
        private List<TResult> _choices;

        private IConsole _console;

        private IConvertToStringTrait<TResult> _convert;

        public DisplayChoices(List<TResult> choices, IConvertToStringTrait<TResult> convert, IConsole console)
        {
            _choices = choices;
            _convert = convert;
            _console = console;
        }

        public void Render()
        {
            int index = 0;
            foreach (TResult choice in _choices)
            {
                _console.PositionWriteLine($"   {_convert.Convert.Run(choice)}", 0, index + Consts.CURSOR_OFFSET);
                index++;
            }
        }

        public void Select(int index)
        {
            _console.PositionWriteLine($"-> {_convert.Convert.Run(_choices[index])}", 0, index + Consts.CURSOR_OFFSET, ConsoleColor.DarkYellow);
        }

        public void UnSelect(int index)
        {
            _console.PositionWriteLine($"   {_convert.Convert.Run(_choices[index])}", 0, index + Consts.CURSOR_OFFSET);
        }
    }
}
