using System;
using InquirerCS.Interfaces;

namespace InquirerCS.Components
{
    public class OnEscape : IOnKey
    {
        public OnEscape()
        {
        }

        public bool IsInterrupted { get; set; }

        public void OnKey(ConsoleKey? key)
        {
            if (key == ConsoleKey.Escape)
            {
                IsInterrupted = true;
            }
        }
    }
}