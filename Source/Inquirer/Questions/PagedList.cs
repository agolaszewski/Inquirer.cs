using System;
using System.Linq;
using InquirerCS.Components;
using InquirerCS.Interfaces;

namespace InquirerCS.Questions
{
    public class PagedList<TResult> : IQuestion<TResult>
    {
        private const int _CURSOR_OFFSET = 2;

        private IConfirmComponent<TResult> _confirmComponent;

        private int _cursorPosition = _CURSOR_OFFSET;

        private IDisplayQuestionComponent _displayQuestion;

        private IDisplayErrorComponent _errorComponent;

        private IWaitForInputComponent<StringOrKey> _input;

        private IPagingComponent<TResult> _pagingComponent;

        private IParseComponent<int, TResult> _parseComponent;

        private IRenderChoices<TResult> _renderChoices;

        private IValidateComponent<TResult> _validationComponent;

        public PagedList(
            IPagingComponent<TResult> pagingComponent,
            IConfirmComponent<TResult> confirmComponent,
            IDisplayQuestionComponent displayQuestion,
            IWaitForInputComponent<StringOrKey> inputComponent,
            IParseComponent<int, TResult> parseComponent,
            IRenderChoices<TResult> renderChoices,
            IValidateComponent<TResult> validationComponent,
            IDisplayErrorComponent errorComponent)
        {
            _pagingComponent = pagingComponent;
            _confirmComponent = confirmComponent;
            _displayQuestion = displayQuestion;
            _input = inputComponent;
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
            _renderChoices.Select(_cursorPosition - _CURSOR_OFFSET);

            int boundryTop = 2;
            int boundryBottom = boundryTop + _pagingComponent.CurrentPage.Count() - 1;

            while (true)
            {
                var keyPressed = _input.WaitForInput().InterruptKey;
                switch (keyPressed)
                {
                    case (ConsoleKey.LeftArrow):
                        {
                            _pagingComponent.Previous();
                            return Prompt();
                        }

                    case (ConsoleKey.RightArrow):
                        {
                            _pagingComponent.Next();
                            return Prompt();
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

                _renderChoices.Render();
                _renderChoices.Select(_cursorPosition - _CURSOR_OFFSET);
            }

        Escape:
            TResult result = _parseComponent.Parse(_cursorPosition - _CURSOR_OFFSET);
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
