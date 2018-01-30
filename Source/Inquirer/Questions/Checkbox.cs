using System;
using System.Collections.Generic;
using InquirerCS.Components;
using InquirerCS.Interfaces;

namespace InquirerCS.Questions
{
    public class Checkbox<TList, TResult> : IQuestion<TList> where TList : IEnumerable<TResult>
    {
        private List<Selectable<TResult>> _choices;

        private IConfirmComponent<TList> _confirmComponent;

        private IRenderQuestionComponent _displayQuestionComponent;

        private IDisplayErrorComponent _errorComponent;

        private IWaitForInputComponent<StringOrKey> _input;

        private IOnKey _onKey;

        private IParseComponent<List<Selectable<TResult>>, TList> _parseComponent;

        private IRenderChoices<TResult> _renderchoices;

        private IValidateComponent<TList> _validationComponent;

        public Checkbox(
            List<Selectable<TResult>> choices,
            IConfirmComponent<TList> confirmComponent,
            IRenderQuestionComponent displayQuestion,
            IWaitForInputComponent<StringOrKey> inputComponent,
            IParseComponent<List<Selectable<TResult>>, TList> parseComponent,
            IRenderChoices<TResult> renderChoices,
            IValidateComponent<TList> validationComponent,
            IDisplayErrorComponent errorComponent,
            IOnKey onKey)
        {
            _choices = choices;
            _confirmComponent = confirmComponent;
            _displayQuestionComponent = displayQuestion;
            _input = inputComponent;
            _parseComponent = parseComponent;
            _renderchoices = renderChoices;
            _validationComponent = validationComponent;
            _errorComponent = errorComponent;
            _onKey = onKey;

            Console.CursorVisible = false;
        }

        public TList Prompt()
        {
            _displayQuestionComponent.Render();

            if (_choices.Count == 0)
            {
                _errorComponent.Render("No choices");
                var keyPressed = _input.WaitForInput().InterruptKey;
                _onKey.OnKey(keyPressed);
                return default(TList);
            }

            _renderchoices.Render();
            _renderchoices.Select(0);

            int boundryTop = Consts.CURSOR_OFFSET;
            int boundryBottom = boundryTop + _choices.Count - 1;

            int cursorPosition = Consts.CURSOR_OFFSET;

            while (true)
            {
                var keyPressed = _input.WaitForInput().InterruptKey;
                _onKey.OnKey(keyPressed);
                if (_onKey.IsInterrupted)
                {
                    return default(TList);
                }

                switch (keyPressed)
                {
                    case ConsoleKey.Spacebar:
                        {
                            _choices[cursorPosition - Consts.CURSOR_OFFSET].IsSelected ^= true;
                            _renderchoices.Select(cursorPosition - Consts.CURSOR_OFFSET);

                            break;
                        }

                    case ConsoleKey.UpArrow:
                        {
                            if (cursorPosition > boundryTop)
                            {
                                _renderchoices.UnSelect(cursorPosition - Consts.CURSOR_OFFSET);
                                cursorPosition -= 1;
                            }

                            _renderchoices.Select(cursorPosition - Consts.CURSOR_OFFSET);

                            break;
                        }

                    case ConsoleKey.DownArrow:
                        {
                            if (cursorPosition < boundryBottom)
                            {
                                _renderchoices.UnSelect(cursorPosition - Consts.CURSOR_OFFSET);
                                cursorPosition += 1;
                            }

                            _renderchoices.Select(cursorPosition - Consts.CURSOR_OFFSET);

                            break;
                        }

                    case ConsoleKey.Enter:
                        {
                            goto Escape;
                        }
                }
            }

            Escape:
            TList result = _parseComponent.Parse(_choices);
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
