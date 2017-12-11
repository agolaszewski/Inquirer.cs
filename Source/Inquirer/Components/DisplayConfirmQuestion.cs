using System;
using InquirerCS.Interfaces;
using InquirerCS.Traits;

namespace InquirerCS.Components
{
    public class DisplayConfirmQuestion<TResult> : IRenderQuestionComponent
    {
        private IConvertToStringTrait<TResult> _convert;

        private IDefaultTrait<TResult> _default;

        private string _message;

        public DisplayConfirmQuestion(string message, IConvertToStringTrait<TResult> convert, IDefaultTrait<TResult> @default)
        {
            _message = message;
            _convert = convert;
            _default = @default;
        }

        public void Render()
        {
            Console.Clear();
            ConsoleHelper.Write("[?] ", ConsoleColor.Yellow);
            var question = $"{_message} [y/n] : ";
            if (_default.Default.HasDefault)
            {
                question += $"[{_convert.Convert.Run(_default.Default.Value)}] ";
            }

            Console.Write(question);
        }
    }
}
