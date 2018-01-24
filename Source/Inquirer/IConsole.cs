using System;

namespace InquirerCS
{
    public interface IConsole
    {
        int CursorLeft { get; }

        int CursorTop { get; }

        void Clear();

        void PositionWrite(string text, int x = 0, int y = 0, ConsoleColor color = ConsoleColor.White);

        void PositionWriteLine(string text, int x = 0, int y = 0, ConsoleColor color = ConsoleColor.White);

        string Read();

        string Read(out ConsoleKey? intteruptedKey, Func<char, bool> allowTypeFn, params ConsoleKey[] interruptKeys);

        ConsoleKeyInfo ReadKey();

        ConsoleKeyInfo ReadKey(out bool isCanceled);

        void SetCursorPosition(int v, int cursorTop);

        void Write(string text, ConsoleColor color = ConsoleColor.White);

        void WriteError(string error);

        void WriteLine(string text = " ", ConsoleColor color = ConsoleColor.White);
    }
}
