using System;
using System.Collections.Generic;
using InquirerCS.Components;
using InquirerCS.Interfaces;

namespace InquirerCS.Questions
{
    public class PagedCheckbox<TList, TResult> : IQuestion<TList> where TList : IEnumerable<TResult>
    {
        private const int _CURSOR_OFFSET = 2;

        private IConfirmComponent<TList> _confirmComponent;

        private int _cursorPosition = _CURSOR_OFFSET;

        private IDisplayQuestionComponent _displayQuestionComponent;

        private IDisplayErrorComponent _errorComponent;

        private IWaitForInputComponent<StringOrKey> _input;

        private IOnKey _onKey;

        private IPagingComponent<Selectable<TResult>> _pagingComponent;

        private IParseComponent<Dictionary<int, List<Selectable<TResult>>>, TList> _parseComponent;

        private IRenderChoices<TResult> _renderchoices;

        private IValidateComponent<TList> _validationComponent;

        public PagedCheckbox(
            IPagingComponent<Selectable<TResult>> pagingComponent,
            IConfirmComponent<TList> confirmComponent,
            IDisplayQuestionComponent displayQuestion,
             IWaitForInputComponent<StringOrKey> inputComponent,
            IParseComponent<Dictionary<int, List<Selectable<TResult>>>, TList> parseComponent,
            IRenderChoices<TResult> renderChoices,
            IValidateComponent<TList> validationComponent,
            IDisplayErrorComponent errorComponent,
              IOnKey onKey)
        {
            _pagingComponent = pagingComponent;
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
            _renderchoices.Render();
            _renderchoices.Select(_cursorPosition - _CURSOR_OFFSET);

            int boundryTop = 2;
            int boundryBottom = boundryTop + _pagingComponent.CurrentPage.Count - 1;

            while (true)
            {
                var keyPressed = _input.WaitForInput().InterruptKey;
                _onKey.OnKey(keyPressed);

                switch (keyPressed)
                {
                    case ConsoleKey.Spacebar:
                        {
                            _pagingComponent.CurrentPage[_cursorPosition - _CURSOR_OFFSET].IsSelected ^= true;
                            break;
                        }

                    case ConsoleKey.LeftArrow:
                        {
                            if (_pagingComponent.Previous())
                            {
                                return Prompt();
                            }

                            break;
                        }

                    case ConsoleKey.RightArrow:
                        {
                            if (_pagingComponent.Next())
                            {
                                _cursorPosition = MathHelper.Clamp(_cursorPosition, _CURSOR_OFFSET, _pagingComponent.CurrentPage.Count - 1 + _CURSOR_OFFSET);
                                return Prompt();
                            }

                            break;
                        }

                    case ConsoleKey.UpArrow:
                        {
                            if (_cursorPosition > boundryTop)
                            {
                                _cursorPosition -= 1;
                            }
                            else
                            {
                                if (_pagingComponent.Previous())
                                {
                                    _cursorPosition = _pagingComponent.CurrentPage.Count - 1 + _CURSOR_OFFSET;
                                    return Prompt();
                                }
                            }

                            break;
                        }

                    case ConsoleKey.DownArrow:
                        {
                            if (_cursorPosition < boundryBottom)
                            {
                                _cursorPosition += 1;
                            }
                            else
                            {
                                if (_pagingComponent.Next())
                                {
                                    _cursorPosition = _CURSOR_OFFSET;
                                    return Prompt();
                                }
                            }

                            break;
                        }

                    case ConsoleKey.Enter:
                        {
                            goto Escape;
                        }
                }

                _renderchoices.Render();
                _renderchoices.Select(_cursorPosition - _CURSOR_OFFSET);
            }

        Escape:
            TList result = _parseComponent.Parse(_pagingComponent.PagedChoices);
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
