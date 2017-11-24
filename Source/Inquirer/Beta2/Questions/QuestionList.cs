using System;
using System.Linq;
using InquirerCS.Beta2.Interfaces;

namespace InquirerCS.Beta2.Questions
{
    public class QuestionList<TResult> : IQuestion<TResult>
    {
        private const int _CURSOR_OFFSET = 2;

        private IChoicesComponent<TResult> _choicesComponent;

        private IRenderComponent _displayQuestion;

        private IWaitForInputComponent<ConsoleKey> _inputComponent;

        private IRenderChoicesComponent<TResult> _renderChoices;

        public QuestionList()
        {
        }

        public void Register(IChoicesComponent<TResult> choicesComponent)
        {
            _choicesComponent = choicesComponent;
        }

        public void Register(IRenderChoicesComponent<TResult> renderChoices)
        {
            _renderChoices = renderChoices;
        }

        public void Register(IRenderComponent displayQuestion)
        {
            _displayQuestion = displayQuestion;
        }

        public void Register(IWaitForInputComponent<ConsoleKey> inputComponent)
        {
            _inputComponent = inputComponent;
        }

        public TResult Prompt()
        {
            _displayQuestion.Render();
            _renderChoices.Render();

            int boundryTop = 2;
            int boundryBottom = boundryTop + _choicesComponent.Choices.Count() - 1;

            while (true)
            {
                int cursorPosition = _CURSOR_OFFSET;
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

                            break;
                        }

                    case ConsoleKey.Enter:
                        {
                            goto Escape;
                        }
                }
            }

            Escape:
            return default(TResult);
        }
    }
}