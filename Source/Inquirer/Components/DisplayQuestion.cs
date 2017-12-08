using System;
using InquirerCS.Interfaces;
using InquirerCS.Traits;

namespace InquirerCS.Components
{
    public class DisplayQuestion<TResult> : IRenderQuestionComponent
    {
        private IConvertToStringTrait<TResult> _convert;
        private IDefaultTrait<TResult> _default;

        private string _message;

        public DisplayQuestion(string message, IConvertToStringTrait<TResult> convert, IDefaultTrait<TResult> @default)
        {
            _message = message;
            _convert = convert;
            _default = @default;
        }

        public void Render()
        {
            Console.Clear();
            ConsoleHelper.Write("[?] ", ConsoleColor.Yellow);
            var question = $"{_message} : ";
            if (_default.Default.HasDefault)
            {
                question += $"[{_convert.Convert.Run(_default.Default.Value)}] ";
            }

            Console.Write(question);
        }
    }
}