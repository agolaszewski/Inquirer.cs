using System;
using System.Linq;
using InquirerCS.Beta2.Interfaces;

namespace InquirerCS.Beta2.Questions
{
    public class QuestionList<TResult> : IQuestion<TResult>
    {
        private const int _CURSOR_OFFSET = 2;

        private IChoicesComponent<TResult> _choicesComponent;

        private IConfirmComponent<TResult> _confirmComponent;

        private IRenderComponent _displayQuestion;

        private IWaitForInputComponent<ConsoleKey> _inputComponent;

        private IParseComponent<int, TResult> _parseComponent;

        private IRenderChoicesComponent<TResult> _renderChoices;

        private IValidateComponent<TResult> _validationComponent;

        public QuestionList()
        {
        }

        public TResult Prompt()
        {
            _displayQuestion.Render();
            _renderChoices.Render();

            int boundryTop = 2;
            int boundryBottom = boundryTop + _choicesComponent.Choices.Count() - 1;

            int cursorPosition = _CURSOR_OFFSET;

            while (true)
            {
                var keyPressed = _inputComponent.WaitForInput();
                switch (keyPressed)
                {
                    case ConsoleKey.UpArrow:
                        {
                            if (cursorPosition > boundryTop)
                            {
                                cursorPosition -= 1;
                            }

                            _renderChoices.Render();
                            _renderChoices.Select(cursorPosition - _CURSOR_OFFSET);

                            break;
                        }

                    case ConsoleKey.DownArrow:
                        {
                            if (cursorPosition < boundryBottom)
                            {
                                cursorPosition += 1;
                            }

                            _renderChoices.Render();
                            _renderChoices.Select(cursorPosition - _CURSOR_OFFSET);

                            break;
                        }

                    case ConsoleKey.Enter:
                        {
                            goto Escape;
                        }
                }
            }

        Escape:
            TResult result = _parseComponent.Parse(cursorPosition - _CURSOR_OFFSET);
            if (!_validationComponent.Run(result))
            {
                return Prompt();
            }

            if (!_confirmComponent.Run(result))
            {
                return Prompt();
            }

            return result;
        }

        public void Register(IChoicesComponent<TResult> choicesComponent)
        {
            _choicesComponent = choicesComponent;
        }

        public void Register(IParseComponent<int, TResult> parseComponent)
        {
            _parseComponent = parseComponent;
        }

        public void Register(IRenderChoicesComponent<TResult> renderChoices)
        {
            _renderChoices = renderChoices;
        }

        public void Register(IRenderComponent displayQuestion)
        {
            _displayQuestion = displayQuestion;
        }

        public void Register(IValidateComponent<TResult> validationComponent)
        {
            _validationComponent = validationComponent;
        }

        public void Register(IConfirmComponent<TResult> confirmComponent)
        {
            _confirmComponent = confirmComponent;
        }

        public void Register(IWaitForInputComponent<ConsoleKey> inputComponent)
        {
            _inputComponent = inputComponent;
        }
    }
}