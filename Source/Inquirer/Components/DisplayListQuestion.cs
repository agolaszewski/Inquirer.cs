using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InquirerCS.Interfaces;
using InquirerCS.Traits;

namespace InquirerCS.Components
{
    public class DisplayListQuestion<TList, TResult> : IRenderQuestionComponent where TList : IEnumerable<TResult>
    {
        private IConvertToStringTrait<TResult> _convert;

        private IDefaultTrait<List<TResult>> _default;

        private string _message;

        public DisplayListQuestion(string message, IConvertToStringTrait<TResult> convert, IDefaultTrait<List<TResult>> @default)
        {
            _message = message;
            _convert = convert;
            _default = @default;
        }

        public void Render()
        {
            StringBuilder sb = new StringBuilder();

            Console.Clear();
            AppConsole2.Write("[?] ", ConsoleColor.Yellow);

            sb.Append($"{_message} : ");
            if (_default.Default.HasDefault)
            {
                sb.Append("[");
                sb.Append(string.Join(", ", _default.Default.Value.Select(item => _convert.Convert.Run(item))));
                sb.Append("]");
            }

            Console.Write(sb.ToString());
        }
    }
}
