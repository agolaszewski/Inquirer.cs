using System;

namespace InquirerCS.Interfaces
{
    public interface IOnKey
    {
        bool IsInterrupted { get; }

        void OnKey(ConsoleKey? key);
    }
}
