using System;
using InquirerCS.Interfaces;

namespace InquirerCS.Components
{
    internal class OnEscape : IOnKey
    {
        public OnEscape()
        {
        }

        public bool IsInterrupted { get; set; }

        public void OnKey(ConsoleKey? key)
        {
            IsInterrupted = key == ConsoleKey.Escape;
        }
    }
}
