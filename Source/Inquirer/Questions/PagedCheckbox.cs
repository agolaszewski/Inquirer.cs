using System;
using System.Collections.Generic;
using InquirerCS.Components;
using InquirerCS.Interfaces;

namespace InquirerCS.Questions
{
    public class PagedCheckbox<TList, TResult> : IQuestion<TList> where TList : IEnumerable<TResult>
    {
        private const int _CURSOR_OFFSET = 2;

        private Dictionary<int, List<Selectable<TResult>>> _choices;

        private IConfirmComponent<TList> _confirmComponent;

        private IDisplayQuestionComponent _displayQuestionComponent;

        private IDisplayErrorComponent _errorComponent;

        private IWaitForInputComponent<ConsoleKey> _inputComponent;

        private IPagingComponent<Selectable<TResult>> _pagingComponent;

        private IParseComponent<Dictionary<int, List<Selectable<TResult>>>, TList> _parseComponent;

        private IRenderChoices<TResult> _renderchoices;

        private IValidateComponent<TList> _validationComponent;

        public PagedCheckbox(
            Dictionary<int, List<Selectable<TResult>>> choices,
            IPagingComponent<Selectable<TResult>> pagingComponent,
            IConfirmComponent<TList> confirmComponent,
            IDisplayQuestionComponent displayQuestion,
            IWaitForInputComponent<ConsoleKey> inputComponent,
            IParseComponent<Dictionary<int, List<Selectable<TResult>>>, TList> parseComponent,
            IRenderChoices<TResult> renderChoices,
            IValidateComponent<TList> validationComponent,
            IDisplayErrorComponent errorComponent)
        {
            _choices = choices;
            _pagingComponent = pagingComponent;
            _confirmComponent = confirmComponent;
            _displayQuestionComponent = displayQuestion;
            _inputComponent = inputComponent;
            _parseComponent = parseComponent;
            _renderchoices = renderChoices;
            _validationComponent = validationComponent;
            _errorComponent = errorComponent;

            Console.CursorVisible = false;
        }

        public TList Prompt()
        {
            _displayQuestionComponent.Render();
            _renderchoices.Render();
            _renderchoices.Select(0);

            int boundryTop = 2;
            int boundryBottom = boundryTop + _choices[_pagingComponent.CurrentPageNumber].Count - 1;

            int cursorPosition = _CURSOR_OFFSET;

            while (true)
            {
                var keyPressed = _inputComponent.WaitForInput();
                switch (keyPressed)
                {
                    case ConsoleKey.Spacebar:
                        {
                            _choices[_pagingComponent.CurrentPageNumber][cursorPosition - _CURSOR_OFFSET].IsSelected ^= true;
                            _renderchoices.Render();
                            _renderchoices.Select(cursorPosition - _CURSOR_OFFSET);

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
                                return Prompt();
                            }

                            break;
                        }

                    case ConsoleKey.UpArrow:
                        {
                            if (cursorPosition > boundryTop)
                            {
                                cursorPosition -= 1;
                            }
                            else
                            {
                                if (_pagingComponent.Previous())
                                {
                                    return Prompt();
                                }
                            }

                            _renderchoices.Render();
                            _renderchoices.Select(cursorPosition - _CURSOR_OFFSET);

                            break;
                        }

                    case ConsoleKey.DownArrow:
                        {
                            if (cursorPosition < boundryBottom)
                            {
                                cursorPosition += 1;
                            }
                            else
                            {
                                if (_pagingComponent.Next())
                                {
                                    return Prompt();
                                }
                            }

                            _renderchoices.Render();
                            _renderchoices.Select(cursorPosition - _CURSOR_OFFSET);

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
