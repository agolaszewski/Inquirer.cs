using System;
using System.Linq;
using InquirerCS.Components;
using InquirerCS.Interfaces;

namespace InquirerCS.Questions
{
    public class PagedList<TResult> : IQuestion<TResult>
    {
        private IConfirmComponent<TResult> _confirmComponent;

        private int _cursorPosition = -1;

        private IRenderQuestionComponent _displayQuestion;

        private IDisplayErrorComponent _errorComponent;

        private IWaitForInputComponent<StringOrKey> _input;

        private IOnKey _onKey;

        private IPagingComponent<TResult> _pagingComponent;

        private IParseComponent<int, TResult> _parseComponent;

        private IRenderChoices<TResult> _renderChoices;

        private IValidateComponent<TResult> _validationComponent;

        public PagedList(
            IPagingComponent<TResult> pagingComponent,
            IConfirmComponent<TResult> confirmComponent,
            IRenderQuestionComponent displayQuestion,
            IWaitForInputComponent<StringOrKey> inputComponent,
            IParseComponent<int, TResult> parseComponent,
            IRenderChoices<TResult> renderChoices,
            IValidateComponent<TResult> validationComponent,
            IDisplayErrorComponent errorComponent,
              IOnKey onKey)
        {
            _pagingComponent = pagingComponent;
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

            if (_pagingComponent.PagedChoices.Count == 0)
            {
                _errorComponent.Render("No choices");
                var keyPressed = _input.WaitForInput().InterruptKey;
                _onKey.OnKey(keyPressed);
                return default(TResult);
            }

            _renderChoices.Render();

            if (_cursorPosition < 0)
            {
                _cursorPosition = Consts.CURSOR_OFFSET;
            }

            _renderChoices.Select(_cursorPosition - Consts.CURSOR_OFFSET);

            int boundryTop = Consts.CURSOR_OFFSET;
            int boundryBottom = boundryTop + _pagingComponent.CurrentPage.Count() - 1;

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
                                    _cursorPosition = _pagingComponent.CurrentPage.Count - 1 + Consts.CURSOR_OFFSET;
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
                                    _cursorPosition = Consts.CURSOR_OFFSET;
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
                _renderChoices.Select(_cursorPosition - Consts.CURSOR_OFFSET);
            }

            Escape:
            TResult result = _parseComponent.Parse(_cursorPosition - Consts.CURSOR_OFFSET);
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
