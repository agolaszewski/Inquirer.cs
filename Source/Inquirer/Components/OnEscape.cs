using System;
using InquirerCS.Interfaces;

namespace InquirerCS.Components
{
    public class OnEscape : IOnKey
    {
        private Inquirer _inquirer;

        public OnEscape(Inquirer inquirer)
        {
            _inquirer = inquirer;
        }

        public void OnKey(ConsoleKey? key)
        {
            if (key == ConsoleKey.Escape)
            {
                throw new OperationCanceledException();
            }
        }
    }
}
