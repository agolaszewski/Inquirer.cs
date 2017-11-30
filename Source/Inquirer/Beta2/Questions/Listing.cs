using System;
using System.Collections.Generic;
using System.Linq;
using InquirerCS.Beta2.Interfaces;

namespace InquirerCS.Beta2.Questions
{
    public class Listing<TResult> : IQuestion<TResult>
    {
        private const int _CURSOR_OFFSET = 2;

        private List<TResult> _choices;

        private IConfirmComponent<TResult> _confirmComponent;

        private IDisplayQuestionComponent _displayQuestion;

        private IWaitForInputComponent<ConsoleKey> _inputComponent;

        private IParseComponent<int, TResult> _parseComponent;

        private IRenderchoices<TResult> _renderChoices;

        private IValidateComponent<TResult> _validationComponent;

        private IDisplayErrorComponent _errorComponent;

        public Listing(
            List<TResult> choices,
            IConfirmComponent<TResult> confirmComponent,
            IDisplayQuestionComponent displayQuestion,
            IWaitForInputComponent<ConsoleKey> inputComponent,
            IParseComponent<int, TResult> parseComponent,
            IRenderchoices<TResult> renderChoices,
            IValidateComponent<TResult> validationComponent,
            IDisplayErrorComponent errorComponent)
        {
            _choices = choices;
            _confirmComponent = confirmComponent;
            _displayQuestion = displayQuestion;
            _inputComponent = inputComponent;
            _parseComponent = parseComponent;
            _renderChoices = renderChoices;
            _validationComponent = validationComponent;
            _errorComponent = errorComponent;

            Console.CursorVisible = false;
        }

        public TResult Prompt()
        {
            _displayQuestion.Render();
            _renderChoices.Render();
            _renderChoices.Select(0);

            int boundryTop = 2;
            int boundryBottom = boundryTop + _choices.Count() - 1;

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
            var validationResult = _validationComponent.Run(result);
            if (validationResult.HasError)
            {
                _errorComponent.Render(validationResult.ErrorMessage);
                return Prompt();
            }

            if (_confirmComponent.Confirm(result))
            {
                return Prompt();
            }

            return result;
        }
    }
}