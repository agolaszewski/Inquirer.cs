using System;
using InquirerCS.Interfaces;
using InquirerCS.Traits;

namespace InquirerCS.Components
{
    internal class RenderQuestion<TResult> : IRenderQuestionComponent
    {
        private IConsole _console;

        private IConvertToStringTrait<TResult> _convert;

        private IDefaultTrait<TResult> _default;

        private string _message;

        public RenderQuestion(string message, IConvertToStringTrait<TResult> convert, IDefaultTrait<TResult> @default, IConsole console)
        {
            _message = message;
            _convert = convert;
            _default = @default;
            _console = console;
        }

        public void Render()
        {
            _console.Clear();
            _console.Write("[?] ", ConsoleColor.Yellow);
            var question = $"{_message} : ";
            if (_default.Default.HasDefault)
            {
                question += $"[{_convert.Convert.Run(_default.Default.Value)}] ";
            }

            _console.Write(question);
            Consts.CURSOR_OFFSET = _console.CursorTop + 2;
        }
    }
}
