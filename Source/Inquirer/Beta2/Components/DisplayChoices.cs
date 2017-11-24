using System.Linq;
using InquirerCS.Beta2.Interfaces;

namespace InquirerCS.Beta2.Components
{
    public class DisplayChoices<TResult> : IRenderChoicesComponent<TResult>
    {
        private const int _CURSOR_OFFSET = 2;

        private IChoicesComponent<TResult> _choicesComponent;

        private IConvertToStringComponent<TResult> _convertToString;

        public DisplayChoices()
        {
        }

        public void Register(IChoicesComponent<TResult> choicesComponent)
        {
            _choicesComponent = choicesComponent;
        }

        public void Register(IConvertToStringComponent<TResult> convertToString)
        {
            _convertToString = convertToString;
        }

        public void Render()
        {
            int index = 0;
            foreach (TResult choice in _choicesComponent.Choices)
            {
                ConsoleHelper.PositionWriteLine($"{_convertToString.Convert(choice)}", _CURSOR_OFFSET, index + _CURSOR_OFFSET);
                index++;
            }
        }

        public void Select(int index)
        {
            ConsoleHelper.PositionWriteLine($"-> {_convertToString.Convert(_choicesComponent.Choices.ElementAt(index))}", 0, index + _CURSOR_OFFSET);
        }
    }
}
