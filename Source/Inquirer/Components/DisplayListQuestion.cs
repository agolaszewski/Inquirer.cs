using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InquirerCS.Interfaces;
using InquirerCS.Traits;

namespace InquirerCS.Components
{
    internal class DisplayListQuestion<TList, TResult> : IRenderQuestionComponent where TList : IEnumerable<TResult>
    {
        private IConsole _console;

        private IConvertToStringTrait<TResult> _convert;

        private IDefaultTrait<List<TResult>> _default;

        private string _message;

        public DisplayListQuestion(string message, IConvertToStringTrait<TResult> convert, IDefaultTrait<List<TResult>> @default, IConsole console)
        {
            _message = message;
            _convert = convert;
            _default = @default;
            _console = console;
        }

        public void Render()
        {
            StringBuilder sb = new StringBuilder();

            _console.Clear();
            _console.Write("[?] ", ConsoleColor.Yellow);

            sb.Append($"{_message} : ");
            if (_default.Default.HasDefault)
            {
                sb.Append("[");
                sb.Append(string.Join(", ", _default.Default.Value.Select(item => _convert.Convert.Run(item))));
                sb.Append("]");
            }

            _console.Write(sb.ToString());
            Consts.CURSOR_OFFSET = _console.CursorTop + 2;
        }
    }
}
