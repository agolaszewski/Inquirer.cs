using System;
using InquirerCS.Interfaces;

namespace InquirerCS.Components
{
    public class OnNothing : IOnKey
    {
        public void OnKey(ConsoleKey? key)
        {
            return;
        }
    }
}