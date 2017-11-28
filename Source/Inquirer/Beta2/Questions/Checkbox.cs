using System;
using System.Collections.Generic;
using InquirerCS.Beta2.Components;
using InquirerCS.Beta2.Interfaces;

namespace InquirerCS.Beta2.Questions
{
    public class Checkbox<TList, TResult> : IQuestion<TList> where TList : IEnumerable<TResult>
    {
        private const int _CURSOR_OFFSET = 2;

        private IChoicesComponent<Selectable<TResult>> _choicesComponent;

        private IConfirmComponent<TList> _confirmComponent;

        private IDisplayQuestionComponent _displayQuestionComponent;

        private IDisplayErrorComponent _errorComponent;

        private IWaitForInputComponent<ConsoleKey> _inputComponent;

        private IParseComponent<List<Selectable<TResult>>, TList> _parseComponent;

        private IRenderChoicesComponent<TResult> _renderChoicesComponent;

        private IValidateComponent<TList> _validationComponent;

        public Checkbox(
            IChoicesComponent<Selectable<TResult>> choicesComponent,
            IConfirmComponent<TList> confirmComponent,
            IDisplayQuestionComponent displayQuestion,
            IWaitForInputComponent<ConsoleKey> inputComponent,
            IParseComponent<List<Selectable<TResult>>, TList> parseComponent,
            IRenderChoicesComponent<TResult> renderChoices,
            IValidateComponent<TList> validationComponent,
            IDisplayErrorComponent errorComponent)
        {
            _choicesComponent = choicesComponent;
            _confirmComponent = confirmComponent;
            _displayQuestionComponent = displayQuestion;
            _inputComponent = inputComponent;
            _parseComponent = parseComponent;
            _renderChoicesComponent = renderChoices;
            _validationComponent = validationComponent;
            _errorComponent = errorComponent;

            Console.CursorVisible = false;
        }

        public TList Prompt()
        {
            _displayQuestionComponent.Render();
            _renderChoicesComponent.Render();
            _renderChoicesComponent.Select(0);

            int boundryTop = 2;
            int boundryBottom = boundryTop + _choicesComponent.Choices.Count - 1;

            int cursorPosition = _CURSOR_OFFSET;

            while (true)
            {
                var keyPressed = _inputComponent.WaitForInput();
                switch (keyPressed)
                {
                    case ConsoleKey.Spacebar:
                        {
                            _choicesComponent.Choices[cursorPosition - _CURSOR_OFFSET].IsSelected ^= true;
                            _renderChoicesComponent.Render();
                            _renderChoicesComponent.Select(cursorPosition - _CURSOR_OFFSET);

                            break;
                        }

                    case ConsoleKey.UpArrow:
                        {
                            if (cursorPosition > boundryTop)
                            {
                                cursorPosition -= 1;
                            }

                            _renderChoicesComponent.Render();
                            _renderChoicesComponent.Select(cursorPosition - _CURSOR_OFFSET);

                            break;
                        }

                    case ConsoleKey.DownArrow:
                        {
                            if (cursorPosition < boundryBottom)
                            {
                                cursorPosition += 1;
                            }

                            _renderChoicesComponent.Render();
                            _renderChoicesComponent.Select(cursorPosition - _CURSOR_OFFSET);

                            break;
                        }

                    case ConsoleKey.Enter:
                        {
                            goto Escape;
                        }
                }
            }

        Escape:
            TList result = _parseComponent.Parse(_choicesComponent.Choices);
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