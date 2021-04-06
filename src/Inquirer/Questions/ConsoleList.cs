using System;
using System.Collections.Generic;
using System.Linq;
using InquirerCS.Components;
using InquirerCS.Interfaces;

namespace InquirerCS.Questions
{
    public class ConsoleList<TResult> : IQuestion<TResult>
    {
        private List<TResult> _choices;

        private IConfirmComponent<TResult> _confirmComponent;

        private IRenderQuestionComponent _displayQuestion;

        private IDisplayErrorComponent _errorComponent;

        private IWaitForInputComponent<StringOrKey> _input;

        private IOnKey _onKey;

        private IParseComponent<int, TResult> _parseComponent;

        private IRenderChoices<TResult> _renderChoices;

        private IValidateComponent<TResult> _validationComponent;

        public ConsoleList(
            List<TResult> choices,
            IConfirmComponent<TResult> confirmComponent,
            IRenderQuestionComponent displayQuestion,
            IWaitForInputComponent<StringOrKey> inputComponent,
            IParseComponent<int, TResult> parseComponent,
            IRenderChoices<TResult> renderChoices,
            IValidateComponent<TResult> validationComponent,
            IDisplayErrorComponent errorComponent,
            IOnKey onKey)
        {
            _choices = choices;
            _confirmComponent = confirmComponent;
            _displayQuestion = displayQuestion;
            _input = inputComponent;
            _parseComponent = parseComponent;
            _renderChoices = renderChoices;
            _validationComponent = validationComponent;
            _errorComponent = errorComponent;
            _onKey = onKey;

            Console.CursorVisible = false;
        }

        public TResult Prompt()
        {
            _displayQuestion.Render();

            if (_choices.Count == 0)
            {
                _errorComponent.Render("No choices");
                var keyPressed = _input.WaitForInput().InterruptKey;
                _onKey.OnKey(keyPressed);
                return default(TResult);
            }

            _renderChoices.Render();
            _renderChoices.Select(0);

            int boundryTop = Consts.CURSOR_OFFSET;
            int boundryBottom = boundryTop + _choices.Count() - 1;

            int cursorPosition = Consts.CURSOR_OFFSET;

            while (true)
            {
                var keyPressed = _input.WaitForInput().InterruptKey;
                _onKey.OnKey(keyPressed);
                if (_onKey.IsInterrupted)
                {
                    return default(TResult);
                }

                switch (keyPressed)
                {
                    case ConsoleKey.UpArrow:
                        {
                            if (cursorPosition > boundryTop)
                            {
                                cursorPosition -= 1;
                            }

                            _renderChoices.Render();
                            _renderChoices.Select(cursorPosition - Consts.CURSOR_OFFSET);

                            break;
                        }

                    case ConsoleKey.DownArrow:
                        {
                            if (cursorPosition < boundryBottom)
                            {
                                cursorPosition += 1;
                            }

                            _renderChoices.Render();
                            _renderChoices.Select(cursorPosition - Consts.CURSOR_OFFSET);

                            break;
                        }

                    case ConsoleKey.Enter:
                        {
                            goto Escape;
                        }
                }
            }

            Escape:
            TResult result = _parseComponent.Parse(cursorPosition - Consts.CURSOR_OFFSET);
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
